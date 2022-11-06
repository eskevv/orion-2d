using Microsoft.Xna.Framework;
using OrionFramework.Entities;

namespace OrionFramework;

public class EntityFollowEmitter : IEmitter
{
    private Entity _entity;
    
    public EntityFollowEmitter(Entity entity)
    {
        _entity = entity;
    }

    public Vector2 EmitPosition => _entity.Position;
}