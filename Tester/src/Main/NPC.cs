using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrionFramework.Entities;

namespace Tester.Main;

public class Npc : Entity
{
    public Npc(Vector2 position) : base(position)
    {
    }

    public override void Update()
    {
    }

    public override void Draw()
    {
        var t = AssetManager.LoadAsset<Texture2D>("npc");
        var s = new Rectangle(0, 0, 16, 16);
        var o = new Vector2(s.Width / 2, s.Height / 2);
        Batcher.DrawTexture(t, Position, s, origin: o);
    }
}