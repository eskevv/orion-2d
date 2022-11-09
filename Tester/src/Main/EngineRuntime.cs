using System;
using Microsoft.Xna.Framework;
using OrionFramework.BaseEngine;
using OrionFramework.DataStorage;
using OrionFramework.Drawing;
using OrionFramework.Scene;

// TODO: scene entity initializer interfaces, scene scripts(on load, on update)
// TODO: IDrawable, Ui editing language, EngineEditor
// TODO: Dialog box word fits in current page before erase

namespace Tester.Main;

public class EngineRuntime : Engine
{
    public EngineRuntime()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 120);
        ScreenClear = new Color(40, 40, 40);
        Window.IsBorderless = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        Screen.PreferredBackBufferWidth = 1600;
        Screen.PreferredBackBufferHeight = 900;
        Screen.ApplyChanges();

        Camera.Zoom = 1f;
        Camera.SetLimits(0, Screen.Width, 0, Screen.Height * 4);
        Camera.Position = Vector2.Zero;

        DataBank.AddDataType<AnimationData>("Data/animations.json");
        
        SceneManager.AddScene(new GameScene("town"));

        var player = new Player("player", new Vector2(500, 500));
        SceneManager.AddEntityToScene(player);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        SceneManager.Update();
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        // SceneManager.Draw();
        
        Batcher.Begin();
        
        Batcher.DrawCircle(500, 500, 100f, Color.Orange, 2, MathHelper.ToRadians(90));
        
        Batcher.Present();;
    }
}