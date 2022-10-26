using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Orion;

public class UIManager
{
    // -- Properties / Fields

    private readonly List<Button> _buttons;

    // -- Initialization

    public UIManager()
    {
        _buttons = new List<Button>();
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
    }

    public void Draw()
    {
        _buttons.ForEach(x => x.Draw());
    }
}