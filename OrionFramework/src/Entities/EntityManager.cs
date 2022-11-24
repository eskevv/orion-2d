using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using OrionFramework.Helpers;
using OrionFramework.Physics;

namespace OrionFramework.Entities;

public class EntityManager
{
    // -- Properties / Fields

    private readonly List<Entity> _entities = new();

    // -- Public Interface

    public void Add(Entity entity)
    {
        entity.Id = _entities.Count;
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

    public T[] GetAllInArea<T>(Rectangle r) where T : Entity
    {
        var inArea = new List<T>();

        var filtered = _entities.Where(x => x.GetType() == typeof(T));

        foreach (var entity in filtered)
        {
            if (r.ContainsPoint(entity.Position))
                inArea.Add((T)entity);
        }

        return inArea.ToArray();
    }

    public T[] GetAllInArea<T>(Circle c) where T : Entity
    {
        var inArea = new List<T>();

        var filtered = _entities.Where(x => x.GetType() == typeof(T));

        foreach (var entity in filtered)
        {
            if (c.ContainsPoint(entity.Position))
                inArea.Add((T)entity);
        }

        return inArea.ToArray();
    }

    public T[] GetAllOf<T>() where T : Entity
    {
        var output = _entities.Where(x => x.GetType() == typeof(T))
            .Select(x => (T)x);

        return output.ToArray();
    } 
}