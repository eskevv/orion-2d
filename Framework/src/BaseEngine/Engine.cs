using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace OrionFramework;

public class Engine : Game
{
    protected GraphicsDeviceManager GraphicsManager { get; set; } = null!;

    protected SpriteBatch SpriteBatcher { get; set; } = null!;

    protected ShapeBatch ShapeBatcher { get; set; } = null!;

    protected Color ScreenClear { get; set; }

    public Engine()
    {
        GraphicsManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 120);
        ScreenClear = new Color(30, 30, 30);
    }

    protected override void Initialize()
    {
        base.Initialize();

        SpriteBatcher = new SpriteBatch(GraphicsDevice);
        ShapeBatcher = new ShapeBatch(GraphicsDevice);

        AssetManager.Initialize(Content);
        Camera.Initialize(GraphicsDevice);
        Batcher.Initialize(ShapeBatcher, SpriteBatcher);
        Screen.Initialize(GraphicsManager);
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