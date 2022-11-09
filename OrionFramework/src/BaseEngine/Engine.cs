using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OrionFramework.Drawing;
using OrionFramework.Scene;

namespace OrionFramework.BaseEngine;

public abstract class Engine : Game
{
    private readonly GraphicsDeviceManager _graphicsManager;
    private SpriteBatch SpriteBatch { get; set; } = null!;
    private ShapeBatch ShapeBatch { get; set; } = null!;
    protected Color ScreenClear { get; set; }
    
    protected Engine()
    {
        _graphicsManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 120);
        ScreenClear = new Color(30, 30, 30);
    }

    protected override void Initialize()
    {
        base.Initialize();

        SpriteBatch = new SpriteBatch(GraphicsDevice);
        ShapeBatch = new ShapeBatch(GraphicsDevice);

        AssetManager.Initialize(Content);
        Camera.Initialize(GraphicsDevice);
        Batcher.Initialize(ShapeBatch, SpriteBatch);
        Screen.Initialize(_graphicsManager);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Input.Update();
        Time.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

        if (Input.Pressed(Keys.Escape))
            Exit();
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        GraphicsDevice.Clear(ScreenClear);
    }
}