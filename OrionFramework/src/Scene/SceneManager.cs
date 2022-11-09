using System;
using System.Collections.Generic;
using OrionFramework.Entities;
using OrionFramework.UserInterface;

namespace OrionFramework.Scene;

public static class SceneManager
{
    private static List<GameScene> _scenes = new();
    private static GameScene? _currentScene;

    public static UiManager UiManager => _currentScene!.UiManager;
    public static EntityManager EntityManager => _currentScene!.EntityManager;
    public static MapManager MapManager => _currentScene!.MapManager;

    public static void AddUiWindowToScene(UiWindow uiWindow, string? sceneName = null)
    {
        var selectedScene = sceneName is null ? _currentScene : _scenes.Find(x => x.Name == sceneName);
        selectedScene?.UiManager.AddUiWindow(uiWindow);
    }

    public static void AddEntityToScene(Entity entity, string? sceneName = null)
    {
        var selectedScene = sceneName is null ? _currentScene : _scenes.Find(x => x.Name == sceneName);
        selectedScene?.EntityManager.Add(entity);
    }

    public static void AddScene(GameScene gameScene, ISceneInitializer? initializer = null)
    {
        _currentScene ??= gameScene;
        _scenes.Add(gameScene);
        gameScene.Load(initializer);
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