using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrionFramework.Entities;

public abstract class Entity
{
    // -- Properties / Fields

    public int ID { get; set; }
    public string? Tag { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public bool Active { get; set; } = true;
    public Texture2D? Texture { get; set; }

    public Entity(Vector2 position, string? tag = null)
    {
        Position = position;
        Tag = tag;
    }

    // -- Public Interface

    public abstract void Update();

    public abstract void Draw();
}