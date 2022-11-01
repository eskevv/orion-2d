using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tester;

public class Game1 : Engine
{
    private UiManager _uiManager = null!;
    private LevelManager _levelManager = null!;

    public Game1()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
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

        Camera.Zoom = 2f;
        Camera.SetLimits(0, Screen.Width, 0, Screen.Height);
        Camera.Position = Vector2.Zero;

        _uiManager = new UiManager();
        _levelManager = new LevelManager();
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _levelManager.Update();
        _uiManager.Update();
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        Batcher.Begin(Camera.Transform);
        _levelManager.Draw();
        Batcher.Present();

        Batcher.Begin();
        _uiManager.Draw();
        Batcher.Present();
    }
}
