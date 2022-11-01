using System;
using System.Collections.Generic;
using System.Linq;

namespace OrionFramework;

public static class EntityManager
{
    // -- Properties / Fields

    private static List<Entity> _entities = new();
    private static List<Entity> _entitiesRemoved = new();

    // -- Public Interface

    public static void Add(Entity entity)
    {
        entity.ID = _entities.Count;
        _entities.Add(entity);
    }

    public static void Update()
    {
        foreach (var entity in _entities.Reverse<Entity>())
        {
            entity.Update();
            if (!entity.Active)
                _entitiesRemoved.Add(entity);
        }

        foreach (var entity in _entitiesRemoved.Reverse<Entity>())
        {
            _entities.Remove(entity);
            _entitiesRemoved.Remove(entity);
        }
    }

    public static Entity[] GetByTag(string tag)
    {
        return _entities.Where(x => x.Tag == tag).ToArray();
    }

    public static void Draw()
    {
        foreach (var entity in _entities.Reverse<Entity>())
            entity.Draw();
    }
}