using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrionFramework.UserInterface.CellGrid;

public class ItemCell
{
    // -- Properties / Fields

    public Vector2 Position { get; private set; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    public bool IsHovered { get; private set; }

    public Texture2D? Image { get; private set; }

    public string? Label { get; private set; }

    public string? ItemId { get; private set; }

    private HoverEffect _hoverEffect;

    // -- Helpers

    public Vector2 Center => new Vector2(Position.X + Width / 2, Position.Y + Height / 2);

    // -- Initialization

    public ItemCell(int xPos, int yPos, int width, int height, HoverEffect? hoverEffect = null, Texture2D? image = null)
    {
        Position = new Vector2(xPos, yPos);
        Width = width;
        Height = height;
        Image = image;
        _hoverEffect = hoverEffect ?? new ScaledHover(2f);
    }

    // -- Internal Processing

    internal void GrowToCenter(float scale)
    {
        float scaledWidth = (int)(Width * scale);
        float scaledHeight = (int)(Height * scale);
        float xDiff = (scaledWidth - Width) / 2;
        float yDiff = (scaledHeight - Height) / 2;

        Position = new Vector2(Position.X - xDiff, Position.Y - yDiff);
        Width = (int)scaledWidth;
        Height = (int)(scaledHeight);
    }

    internal void ShrinkToCenter(float scale)
    {
        float scaledWidth = (int)(Width / scale);
        float scaledHeight = (int)(Height / scale);
        float xDiff = (Width - scaledWidth) / 2;
        float yDiff = (Height - scaledHeight) / 2;

        Position = new Vector2(Position.X + xDiff, Position.Y + yDiff);
        Width = (int)scaledWidth;
        Height = (int)(scaledHeight);
    }

    // -- Public Interface

    internal void SetHoverEffect(HoverEffect hoverEffect)
    {
        _hoverEffect = hoverEffect;
    }

    internal void SetTo(Texture2D? image, string itemId, string? label = null)
    {
        Image = image;
        Label = label;
        ItemId = itemId;
    }

    internal void Clear()
    {
        Image = null;
        Label = null;
        ItemId = null;
    }

    public void Hovered()
    {
        if (IsHovered)
            return;

        _hoverEffect.OnHover(this);
        IsHovered = true;
    }

    public void Reset()
    {
        if (!IsHovered)
            return;

        _hoverEffect.OnReset(this);
        IsHovered = false;
    }
}