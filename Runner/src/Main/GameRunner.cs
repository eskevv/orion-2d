using System;
using Microsoft.Xna.Framework;
using OrionFramework.BaseEngine;
using OrionFramework.Camera;
using OrionFramework.DataStorage;
using OrionFramework.Scene;

// TODO: scene entity initializer interfaces, scene scripts(on load, on update)
// TODO: IDrawable, Ui editing language, EngineEditor
// TODO: Dialog box word fits in current page before erase
// TODO: rotating weapon abstractions
// TODO: complete ECS implementation with external method of producing them
// TODO: MapGenerator refactor
// TODO: (Physics / collisions) refactor / improvements

namespace Tester.Main;

public class GameRunner : Engine
{
    private void EngineSetup()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 120);
        ScreenClear = new Color(40, 40, 40);
        Window.IsBorderless = true;

        Screen.PreferredBackBufferWidth = 1600;
        Screen.PreferredBackBufferHeight = 900;
        Screen.ApplyChanges();

        Camera.Zoom = 2f;
        Camera.SetLimits(0, Screen.Width, 0, Screen.Height * 4);
        Camera.Position = Vector2.Zero;
    }

    protected override void Initialize()
    {
        base.Initialize();

        EngineSetup();

        DataBank.AddDataType<AnimationData>("Data/animations.json");
        SceneManager.AddScene(new GameScene("town"));

        var player = new Player("player", new Vector2(500, 500), "player");
        var bat = new Monster("bat", new Vector2(400, 600));
        SceneManager.AddEntityToScene(player);
        SceneManager.AddEntityToScene(bat);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        SceneManager.Update();
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        SceneManager.Draw();
    }
}