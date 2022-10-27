using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tester;

public class Game1 : Engine
{
    private GameManager _gameManager = null!;
    private UIManager _uiManager = null!;

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

        Screen.PreferredBackBufferWidth = 1500;
        Screen.PreferredBackBufferHeight = 1000;
        Screen.ApplyChanges();

        _gameManager = new GameManager();
        _uiManager = new UIManager();
    }

    protected override void LoadContent()
    {
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/arrow_01b");
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/arrow_03d");
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/book_01g");
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/fish_01a");
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/hat_01e");
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/key_02b");
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/armor_01b");
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/pearl_01b");
        AssetManager.RegisterAsset<Texture2D>("icons/16x16/shard_01j");
        AssetManager.RegisterAsset<Texture2D>("gui/button_small");
        AssetManager.RegisterAsset<Texture2D>("characters/knight_green/knight_green_idle");
        AssetManager.RegisterAsset<Texture2D>("characters/knight_green/knight_green_run");
        AssetManager.RegisterAsset<Texture2D>("characters/knight_green/knight_green_attack");
        AssetManager.RegisterAsset<Texture2D>("characters/troll/troll_idleA");
        AssetManager.RegisterAsset<Texture2D>("tilesets/topdown");
        AssetManager.RegisterAsset<Texture2D>("particle");
        AssetManager.RegisterAsset<Texture2D>("cursor_normal");
        AssetManager.RegisterAsset<SpriteFont>("gameFont");
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _gameManager.Update();
        _uiManager.Update();
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        Batcher.Begin(Camera.Transform);
        _gameManager.Draw();
        Batcher.Present();

        Batcher.Begin();
        _uiManager.Draw();
        Batcher.Present();
    }
}
