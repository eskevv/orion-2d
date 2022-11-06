using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OrionFramework.Entities;
using OrionFramework.UserInterface;

namespace OrionFramework.Scene;



public class Scene
{
    public string Name { get; }
    public EntityManager EntityManager { get; } = new();
    public MapManager MapManager { get; } = new();
    public UiManager UiManager { get; } = new();

    public Scene(string name)
    {
        Name = name;
    }

    public void Load(ISceneInitializer initializer)
    {
        MapManager.Load(Name);
        initializer.Initialize(MapManager, EntityManager);
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