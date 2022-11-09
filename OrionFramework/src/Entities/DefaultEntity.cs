using Microsoft.Xna.Framework;
using OrionFramework.Drawing;

namespace OrionFramework.Entities;

public class DefaultEntity : Entity
{
    public DefaultEntity(string name, Vector2 position, string? tag = null) : base(name, position, tag)
    {
    }

    public override void Update()
    {
    }

    public override void Draw()
    {
        if (Texture is not null)
            Batcher.DrawTexture(Texture, Position);
    }
}