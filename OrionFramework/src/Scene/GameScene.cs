using OrionFramework.Drawing;
using OrionFramework.UserInterface;

namespace OrionFramework.Scene;

public class GameScene
{
    public string? Name { get; }
    public EntityManager EntityManager { get; } = new();
    public MapManager MapManager { get; } = new();
    public UiManager UiManager { get; } = new();

    public GameScene(string? name = null)
    {
        Name = name;
    }

    public void Load(ISceneInitializer? initializer)
    {
        if (Name is null) return;
        MapManager.Load(Name);

        initializer?.Initialize(this);
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