using Microsoft.Xna.Framework;

namespace Orion;

public class StaticEmitter : IEmitter
{
    public Vector2 EmitPosition { get; }

    public StaticEmitter(float xPos, float yPos)
    {
        EmitPosition = new Vector2(xPos, yPos);
    }
}