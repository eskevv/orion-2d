using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrionFramework.Drawing;
using OrionFramework.Entities;

namespace Tester.Main;

public class EntityWeapon
{
    public Vector2 Position { get; private set; }

    public float Rotation { get; private set; }

    private Vector2 _origin;
    private SpriteEffects _xFlipped = SpriteEffects.None;
    private float _rotation;
    private float _scale = 1f;

    private Entity _owner;

    public EntityWeapon(Entity owner)
    {
        _owner = owner;
        _origin = new Vector2(7, 15);
    }

    private readonly Texture2D _texture = AssetManager.LoadAsset<Texture2D>("sword_1");

    public void Update()
    {
        Position = _owner.Position + new Vector2(7, -3);

        // if (_owner.Velocity != Vector2.Zero)

        // float angle = Position.AngleToPoint(Input.WorldCursor);
        // _rotation = angle + MathHelper.PiOver4;

        // _scale = angle < 0 ? 1f : -1f;
    }

    public void Draw()
    {
        Batcher.DrawLine((int)Position.X, (int)Position.Y, (int)Input.WorldCursor.X, (int)Input.WorldCursor.Y);
        // Batcher.DrawTexture(_texture, Position, origin: _origin, effect: _xFlipped, rotation: _rotation, scale: _scale);

        Batcher.DrawFillCircle((int)(Position.X + _origin.X), (int)(Position.Y + _origin.Y), 8f, Color.Red);
    }
}