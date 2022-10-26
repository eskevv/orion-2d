using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Orion;

public class Game1 : Engine
{
    private GameManager _gameManager = null!;

    public Game1()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 120);
        ScreenClear = new Color(40, 40, 40);
        Window.IsBorderless = true;
    }

    protected override void Initialize()
    {
        GraphicsManager.PreferredBackBufferWidth = 1024;
        GraphicsManager.PreferredBackBufferHeight = 768;
        GraphicsManager.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/arrow_01b");
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/arrow_03d");
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/book_01g");
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/fish_01a");
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/hat_01e");
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/key_02b");
        AssetManager.RegisterAsset<Texture2D>("gui/button_small");
        AssetManager.RegisterAsset<Texture2D>("characters/knight_green/knight_green_idle");
        AssetManager.RegisterAsset<Texture2D>("characters/knight_green/knight_green_run");
        AssetManager.RegisterAsset<Texture2D>("characters/knight_green/knight_green_attack");
        AssetManager.RegisterAsset<Texture2D>("characters/troll/troll_idleA");
        AssetManager.RegisterAsset<Texture2D>("tilesets/topdown");
        AssetManager.RegisterAsset<Texture2D>("particle");
        AssetManager.RegisterAsset<SpriteFont>("gameFont");

        base.LoadContent();

        _gameManager = new GameManager();
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _gameManager.Update();
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        Batcher.Begin(Camera.Transform);
        _gameManager.Draw();
        Batcher.Present();
    }
}
