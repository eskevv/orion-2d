using System.Collections.Generic;

namespace OrionFramework.Physics;

public class World
{
    public List<AABB> Colliders { get; private set; }

    public World()
    {
        Colliders = new List<AABB>();
    }

    public void AddCollidingObject(AABB collider)
    {
        Colliders.Add(collider);
    }
}