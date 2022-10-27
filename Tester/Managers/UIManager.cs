using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tester;

public class UIManager
{
    // -- Properties / Fields

    private readonly List<Button> _buttons;
    public static CellContainer _itemWindow = new(300, 200, 32, 24, 8, 2, 4);
    private readonly Texture2D _cursorTexture;

    // -- Initialization

    public UIManager()
    {
        _buttons = new List<Button>();
        _cursorTexture = AssetManager.GetAsset<Texture2D>("cursor_normal");
    }

    // -- Public Interface

    public Button AddButton(int xPos, int yPos, float scale = 1f)
    {
        var font = AssetManager.GetAsset<SpriteFont>("gameFont");
        _buttons.Add(new Button("button_small", font, xPos, yPos, scale));
        return _buttons[_buttons.Count - 1];
    }

    public void Update()
    {
        _buttons.ForEach(x => x.Update());
        _itemWindow.Update();
    }

    public void Draw()
    {
        _buttons.ForEach(x => x.Draw());
        _itemWindow.Draw(Color.Gray, Color.AliceBlue, Color.DarkGray, Color.AntiqueWhite);

        if (_itemWindow.Drawn)
            Batcher.DrawTexture(_cursorTexture, Input.ScreenCursor, scale: 2f);
    }
}