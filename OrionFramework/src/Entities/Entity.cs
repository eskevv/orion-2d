using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrionFramework.Entities;

public abstract class Entity
{
    public string Name;
    public int Id;
    public string? Tag { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public bool Active { get; set; } = true;
    public Texture2D? Texture { get; set; }

    protected Entity(string name, Vector2 position, string? tag = null)
    {
        Position = position;
        Tag = tag;
        Name = name;
    }

    public abstract void Update();

    public abstract void Draw();
}