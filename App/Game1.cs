using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DewInterface;

public class Game1 : Engine
{
    private CellContainer _cellContainer = null!;
    private ItemInventory _itemInventory = null!;
    private WorldMap _worldMap = null!;
    private GameManager _gameManager = null!;
    private UIManager _ui = null!;

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
        base.Initialize();

        GraphicsManager.PreferredBackBufferWidth = 1024;
        GraphicsManager.PreferredBackBufferHeight = 768;
        GraphicsManager.ApplyChanges();

        Camera.Zoom = 2f;
        Camera.SetLimits(0, GraphicsManager.PreferredBackBufferWidth, 0, GraphicsManager.PreferredBackBufferHeight);
        Camera.Position = Vector2.Zero;
    }

    protected override void LoadContent()
    {
        base.LoadContent();

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

        _ui = new UIManager();
        var button = _ui.AddButton(200, 200, 2f);

        _worldMap = new WorldMap("base_level_01");
        _gameManager = new GameManager();

        _cellContainer = new CellContainer(xPos: 310, yPos: 200, cellSize: 30, cellCount: 40, rowSize: 6, padding: 6, cellGap: 2);
        _cellContainer.SetHoveringEffects(new ScaledHover(1.8f));

        _itemInventory = new ItemInventory(_cellContainer);
        _itemInventory.AddItem("fish", "fish_01a", 90);
        _itemInventory.AddItem("fish", "fish_01a", 90);
        _itemInventory.AddItem("arrow", "arrow_01b", 90);
        _itemInventory.AddItem("hat", "hat_01e", 90);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _cellContainer.Update();
        _gameManager.Update();
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        // world and gameplay
        Batcher.Begin(Camera.Transform);
        _worldMap.Draw();
        _gameManager.Draw();
        Batcher.Present();

        // interface
        // Batcher.Begin();
        // _cellContainer.Draw(Color.Black, Color.WhiteSmoke, Color.DimGray, Color.PaleVioletRed);
        // _ui.Draw();
        // Batcher.Present();
    }
}
