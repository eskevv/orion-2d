using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OrionFramework.Entities;
using OrionFramework.Scene;

namespace Tester.Main;

public class Player : Entity
{
    public Player(Vector2 position) : base(position)
    {
    }

    public override void Update()
    {
        Velocity = Input.RawAxes * 1.2f;
        Position += Velocity;

        Camera.Position += (Position - Camera.Position) * 0.1f;
        
        if (Input.Pressed(Keys.V))
            SceneManager.AddEntityToScene(new Npc(Position));
    }

    public override void Draw()
    {
        var t = AssetManager.LoadAsset<Texture2D>("npc");
        var s = new Rectangle(0, 0, 16, 16);
        var o = new Vector2(s.Width / 2, s.Height / 2);
        Batcher.DrawTexture(t, Position, s, origin: o);
    }
}