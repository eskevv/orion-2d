using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrionFramework.Drawing;

namespace OrionFramework.MapGeneration;

public class MapTile
{
    public string Name { get; set; }
    public Texture2D Texture { get; set; }
    public string ParentLayer { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle SrcRect { get; set; }
    public TileAnimation? Animation { get; set; }

    public MapTile(string name, Texture2D texture, string parentLayer, Vector2 worldPosition, Rectangle srcRect, TileAnimation? animation = null)
    {
        Animation = animation;
        Name = name;
        Texture = texture;
        ParentLayer = parentLayer;
        Position = worldPosition;
        SrcRect = srcRect;
    }

    public void Update()
    {
        if (Animation is not null)
            SrcRect = Animation.SrcRect;
    }

    public void Draw()
    {
        Batcher.DrawTexture(Texture, Position, SrcRect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None);
    }
}