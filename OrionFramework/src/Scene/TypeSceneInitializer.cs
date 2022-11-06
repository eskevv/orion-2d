using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OrionFramework.Entities;

namespace OrionFramework.Scene;

public class TypeSceneInitializer : ISceneInitializer
{
    private readonly Dictionary<string, Type> _entityTypes;

    public TypeSceneInitializer(Dictionary<string, Type> entityTypes)
    {
        _entityTypes = entityTypes;
    }

    public void Initialize(MapManager mapManager, EntityManager entityManager)
    {
        foreach (var item in _entityTypes)
            AddEntitiesOfType(mapManager, entityManager, item);
    }

    private void AddEntitiesOfType(MapManager mapManager, EntityManager entityManager, KeyValuePair<string, Type> item)
    {
        var group = mapManager.Objects.Find(x => x.Name == item.Key);
        if (group is null) return;

        foreach (var entity in group.Objects!)
        {
            var position = new Vector2(entity.X, entity.Y);
            var instance = Activator.CreateInstance(item.Value, position);
            entityManager.Add((Entity)instance!);
        }
    }
}