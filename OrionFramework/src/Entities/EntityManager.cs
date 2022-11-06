using System;
using System.Collections.Generic;
using System.Linq;
using OrionFramework.Entities;

namespace OrionFramework;

public class EntityManager
{
    // -- Properties / Fields

    private List<Entity> _entities = new();

    // -- Public Interface

    public void Add(Entity entity, string? tag = null)
    {
        entity.ID = _entities.Count;
        entity.Tag = tag;
        _entities.Add(entity);
    }

    public void Update()
    {
        foreach (var entity in _entities.Reverse<Entity>())
            entity.Update();
        
        _entities.RemoveAll(x => !x.Active);
    }

    public Entity[] GetByTag(string tag)
    {
        return _entities.Where(x => x.Tag == tag).ToArray();
    }

    public void Draw()
    {
        foreach (var entity in _entities.Reverse<Entity>())
            entity.Draw();
    }
}