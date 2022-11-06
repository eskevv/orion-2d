using System;
using System.Collections.Generic;
using OrionFramework.Entities;
using Tester.Main;

namespace OrionFramework.Scene;

public static class SceneManager
{
    private static List<Scene> _scenes = new();
    private static Scene? _currentScene;

    public static void AddEntityToScene(Entity entity, string? sceneName = null)
    {
        var selectedScene = sceneName is null ? _currentScene : _scenes.Find(x => x.Name == sceneName);
        
        selectedScene?.EntityManager.Add(entity);
    }

    public static void AddScene(Scene scene, Dictionary<string, Type>? initializers = null)
    {
        _currentScene ??= scene;
        _scenes.Add(scene);
        scene.LoadScene(initializers);
    }

    public static void SwitchToScene(string sceneName)
    {
        _currentScene = _scenes.Find(x => x.Name == sceneName);
    }

    public static void Update()
    {
        _currentScene?.Update();
    }

    public static void Draw()
    {
        _currentScene?.Draw();
    }
}