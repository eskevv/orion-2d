using Microsoft.Xna.Framework;

namespace OrionFramework.Particles.Emitters;

public interface IEmitter
{
    Vector2 EmitPosition { get; }
}