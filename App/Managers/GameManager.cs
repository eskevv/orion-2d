using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace DewInterface;

public class GameManager
{
    private readonly IEmitter _particleEmitterRight;
    private readonly Knight _knight;
    private readonly Troll _troll;
    public static List<Rectangle> Walls = new();
    private static World _world = new();

    public static List<AABB> WallColliders => _world.Colliders;

    public GameManager()
    {
        _knight = new Knight(200f, 180f, _world);
        _troll = new Troll(200f, 180f);

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
        _troll.Update();
        _knight.Update();
        ParticleManager.Update();
    }

    public void Draw()
    {
        _troll.Draw();
        _knight.Draw();
        ParticleManager.DrawParticles();

        foreach (var w in WallColliders)
        {
            int xPos = (int)(w.Center.X - w.HalfSize.X);
            int yPos = (int)(w.Center.Y - w.HalfSize.Y);
            var box = new Rectangle(xPos, yPos, (int)(w.HalfSize.X * 2), (int)(w.HalfSize.Y * 2));
            Batcher.DrawRect(box, Color.GhostWhite);
        }
    }
}