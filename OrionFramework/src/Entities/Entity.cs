using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrionFramework.Entities;

public abstract class Entity
{
    public int Id;
    public string? Tag { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public bool Active { get; set; } = true;
    public Texture2D? Texture { get; set; }
    public bool FacingRight { get; set; }

    protected Entity(Vector2? position = null, string? tag = null)
    {
        Position = position ?? Vector2.Zero;
        Tag = tag;
    }

    public abstract void Update();

    public abstract void Draw();
}