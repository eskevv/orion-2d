using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Orion;

public class Button
{
    // -- Properties / Fields

    private readonly Texture2D _texture;
    private readonly Rectangle _srcRect;
    private readonly SpriteFont _spriteFont;
    private Color _tint;
    private Vector2 _position;
    private float _scale;

    public event EventHandler? OnClick;

    // -- Arrow Helpers

    private float HitWidth => _srcRect.Width * _scale;
    private float HitHeight => _srcRect.Height * _scale;

    // -- Initialization

    public Button(string textureName, SpriteFont spriteFont, int xPos, int yPos, float scale = 1f, Rectangle? srcRect = null)
    {
        _texture = AssetManager.GetAsset<Texture2D>(textureName);
        _spriteFont = spriteFont;
        _position = new Vector2(xPos, yPos);
        _scale = scale;
        _srcRect = srcRect ?? new Rectangle(0, 0, _texture.Width, _texture.Height);
        _tint = Color.White;
    }

    // -- Public Interface

    public void Click()
    {
        OnClick?.Invoke(this, EventArgs.Empty);
    }

    public void Update()
    {
        if (Collision.BoxContainsCursor(_position.X, _position.Y, HitWidth, HitHeight) && Input.Pressed(MouseButton.LeftButton))
        {
            _tint = Color.DarkGray;
            Click();
        }
        else
            _tint = Color.White;
    }

    public void Draw()
    {
        Batcher.DrawTexture(_texture, _position, _srcRect, _tint, 0f, null, _scale, SpriteEffects.None);
    }
}
