using Microsoft.Xna.Framework;

namespace OrionFramework.MapGeneration;

public class TileFrame
{
    public readonly Rectangle SrcRect; 
    public readonly float Duration;

    public TileFrame(Rectangle srcRect, int duration)
    {
        SrcRect = srcRect;
        Duration = duration / 1000f;
        
    }
}
