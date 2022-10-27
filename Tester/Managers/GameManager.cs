using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Tester;

public class GameManager
{
    public static List<Loot> Loots = new();
    private static List<Troll> _trolls = new();

    private static World _world = new();
    private MapLevel _mapLevel = new("base_level_01");

    private readonly IEmitter _particleEmitterRight;
    private readonly Knight _knight;
    public static ItemInventory Inventory = new(UIManager._itemWindow);

    public GameManager()
    {
        Camera.Zoom = 3f;
        Camera.SetLimits(0, Screen.Width, 0, Screen.Height);
        Camera.Position = Vector2.Zero;

        _knight = new Knight(150f, 180f, _world);
        _trolls.Add(new Troll(200f, 180f));
        _trolls.Add(new Troll(200f, 220f));
        _trolls.Add(new Troll(100f, 220f));

        Loots.Add(new Loot(new Vector2(80, 130), new Item("chestb", "armor_01b", 80)));
        Loots.Add(new Loot(new Vector2(50, 140), new Item("chestb", "armor_01b", 110)));
        Loots.Add(new Loot(new Vector2(100, 160), new Item("chestb", "armor_01b", 50)));
        Loots.Add(new Loot(new Vector2(60, 130), new Item("fish", "fish_01a", 800), glow: true));
        Loots.Add(new Loot(new Vector2(100, 125), new Item("book", "book_01g", 300), glow: true));

        _particleEmitterRight = new StaticEmitter(1200f, 200);

        _mapLevel.LoadObjects("wall_collisions");

        foreach (var w in _mapLevel.TileColliders)
            _world.AddCollidingObject(w);

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

        Loots.ForEach(x => x.Update());
    }

    public void Draw()
    {
        _mapLevel.Draw();
        _trolls.ForEach(x => x.Draw());
        _knight.Draw();
        ParticleManager.DrawParticles();

        Loots.ForEach(x => x.Draw());
        _mapLevel.TileColliders.ForEach(x => Batcher.DrawRect(x.AsRectangle, Color.Aqua));
    }

    public static void LootItem(Loot loot)
    {
        Inventory.AddItem(loot.Item.Name, loot.Item.ImageName, loot.Item.Value);
        Loots.Remove(loot);
    }

    internal static void FireCollisions(Rectangle x)
    {
        foreach (var troll in _trolls)
            if (x.Intersects(troll._hitRect))
                troll.TakeDamage();
    }
}