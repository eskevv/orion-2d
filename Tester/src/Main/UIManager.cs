using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tester;

public class UiManager
{
    // -- Properties / Fields

    private readonly List<Button> _buttons = new();
    private static readonly CellGrid ItemWindow = new(300, 200, 32, 24, 8, 2, 4);
    private readonly Texture2D _cursorTexture = AssetManager.LoadAsset<Texture2D>("cursor_normal");
    private readonly SpriteFont _globalFont = AssetManager.LoadAsset<SpriteFont>("gameFont");

    // -- Initialization

    public UiManager()
    {
    }

    // -- Public Interface

    public Button AddButton(int xPos, int yPos, float scale = 1f)
    {
        var font = AssetManager.LoadAsset<SpriteFont>("gameFont");
        _buttons.Add(new Button("button_small", font, xPos, yPos, scale));
        return _buttons[^1];
    }

    public void Update()
    {
        _buttons.ForEach(x => x.Update());
        ItemWindow.Update();
    }

    public void Draw()
    {
        _buttons.ForEach(x => x.Draw());
        ItemWindow.Draw(Color.Gray, Color.AliceBlue, Color.DarkGray, Color.AntiqueWhite);

        if (ItemWindow.Drawn)
            Batcher.DrawTexture(_cursorTexture, Input.ScreenCursor, scale: 2f);

        var position_text = LevelManager.FindEntites("player")[0].Position;
        var position_round = new Vector2((int)position_text.X, (int)position_text.Y);
        Batcher.DrawString($"Position: {position_round}", _globalFont, new Vector2(10, 10), Color.Cyan, scale: 2f);
    }
}