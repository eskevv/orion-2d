using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OrionFramework.Entities;
using Tester.Main;

namespace OrionFramework.Scene;

public class Scene
{
    public string Name { get; init; }

    public EntityManager EntityManager { get; } = new();
    public MapManager MapManager { get; } = new();
    public UiManager UiManager { get; } = new();

    public Scene(string name)
    {
        Name = name;
    }

    public void LoadScene(Dictionary<string, Type>? initializers)
    {
        MapManager.Load(Name);

        if (initializers is not null)
            foreach (var item in initializers)
                AddEntitiesOfType(item);
    }

    private void AddEntitiesOfType(KeyValuePair<string, Type> item)
    {
        var group = MapManager.Objects.Find(x => x.Name == item.Key);
        if (group is null) return;

        foreach (var entity in group.Objects!)
        {
            var position = new Vector2(entity.X, entity.Y);
            var instance = Activator.CreateInstance(item.Value, position);
            EntityManager.Add((Entity)instance!);
        }
    }

    public void Update()
    {
        MapManager.Update();
        EntityManager.Update();
        UiManager.Update();
    }

    public void Draw()
    {
        Batcher.Begin(Camera.Transform);
        MapManager.Draw();
        EntityManager.Draw();
        Batcher.Present();

        Batcher.Begin();
        UiManager.Draw();
        Batcher.Present();
    }
}