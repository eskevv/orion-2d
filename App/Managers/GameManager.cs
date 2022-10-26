using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Orion;

public class GameManager
{
    public static List<Rectangle> Walls = new();
    private static List<Troll> _trolls = new();

    private static World _world = new();
    private readonly IEmitter _particleEmitterRight;
    private WorldMap _worldMap = new("base_level_01");
    private readonly Knight _knight;

    public static List<AABB> WallColliders => _world.Colliders;

    public GameManager()
    {
        Camera.Zoom = 3f;
        Camera.SetLimits(0, Screen.Width, 0, Screen.Height);
        Camera.Position = Vector2.Zero;

        _knight = new Knight(150f, 180f, _world);
        _trolls.Add(new Troll(200f, 180f));
        _trolls.Add(new Troll(200f, 220f));
        _trolls.Add(new Troll(100f, 220f));

        _particleEmitterRight = new StaticEmitter(1200f, 200);

        foreach (var w in Walls)
        {
            var position = new Vector2(w.X + w.Width / 2, w.Y + w.Height / 2);
            var box = new AABB(position, w.Width, w.Height);
            _world.AddCollidingObject(box);
        }

        var emitter_dataR = new ParticleEmitterData()
        {
            Interval = 0.5f,
            EmitCount = 60,
            AngleVariance = 100,
            SpeedMax = 70f,
            SpeedMin = 50f,
            LifeSpanMax = 30f,
            LifeSpanMin = 10f,
            Data = new ParticleData()
            {
                ColorStart = Color.WhiteSmoke,
                ColorEnd = Color.DarkBlue,
                SizeStart = 4f,
                SizeEnd = 4f,
                OpacityEnd = 1f,
                Angle = -150
            }
        };
        var emitter1 = new ParticleEmitter(_particleEmitterRight, emitter_dataR);
        ParticleManager.AddParticleEmitter(emitter1);
    }

    public void Update()
    {
        _trolls.ForEach(x => x.Update());
        _knight.Update();
        ParticleManager.Update();
    }

    public void Draw()
    {
        _worldMap.Draw();
        _trolls.ForEach(x => x.Draw());
        _knight.Draw();
        ParticleManager.DrawParticles();

        // foreach (var w in WallColliders)
        // {
        //     int xPos = (int)(w.Center.X - w.HalfSize.X);
        //     int yPos = (int)(w.Center.Y - w.HalfSize.Y);
        //     var box = new Rectangle(xPos, yPos, (int)(w.HalfSize.X * 2), (int)(w.HalfSize.Y * 2));
        //     Batcher.DrawRect(box, Color.GhostWhite);
        // }
    }

    internal static void FireCollisions(Rectangle x)
    {
        foreach (var troll in _trolls)
            if (x.Intersects(troll._hitRect))
                troll.TakeDamage();
    }
}