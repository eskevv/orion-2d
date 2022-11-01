using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace OrionFramework;

public class CharacterData
{
    public int Length;
    public int TotalDuration;
    public List<FrameData> Frames = null!;
    public int offsetX;
    public int offsetY;
}