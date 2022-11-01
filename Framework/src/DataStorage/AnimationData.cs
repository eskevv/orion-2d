using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrionFramework;

public struct AnimationData
{
    public readonly Texture2D Texture;
    public readonly FrameData[] Frames;
    public readonly Vector2 OriginOffset;

    public AnimationData(Texture2D texture, FrameData[] frames, Vector2? originOffset)
    {
        Texture = texture;
        Frames = frames;
        OriginOffset = originOffset ?? Vector2.Zero;
    }
}