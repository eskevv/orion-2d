using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OrionFramework.AssetManagement;
using OrionFramework.Drawing;
using OrionFramework.Helpers;

namespace OrionFramework.UserInterface.CellGrid;

public class CellGrid
{
    // -- Properties / Fields

    private SpriteFont _font = AssetManager.LoadAsset<SpriteFont>("gameFont");
    private List<ItemCell> _cells;
    private int _padding;
    private int _rowSize;
    private int _cellCount;
    private int _cellSize;
    private int _cellGap;

    public Vector2 Position { get; private set; }
    public bool Drawn { get; private set; }

    // -- Helpers

    private int Rows => (_cellCount % _rowSize == 0 ? _cellCount / _rowSize : _cellCount / _rowSize + 1);
    private int ContainerWidth => (_cellSize * _rowSize) + (_rowSize - 1) * _cellGap + _padding * 2;
    private int ContainerHeight => (Rows * _cellSize) + (_cellGap * (Rows - 1)) + _padding * 2;
    private int LeftBorder => (int)Position.X;
    private int TopBorder => (int)Position.Y;
    private int RightBorder => (int)Position.X + ContainerWidth;
    private int BottomBorder => (int)Position.Y + ContainerHeight;
    private bool ContainerHovered => OrionHelp.RectContainsCursor(LeftBorder, TopBorder, RightBorder, BottomBorder);

    // -- Initialization

    public CellGrid(int xPos, int yPos, int cellSize, int cellCount, int rowSize, int cellGap = 5, int padding = 6)
    {
        Position = new Vector2(xPos, yPos);

        _cells = new List<ItemCell>();
        _cellGap = cellGap;
        _cellSize = cellSize;
        _cellCount = cellCount;
        _rowSize = rowSize;
        _padding = padding;

        CreateCells();
    }

    private void CreateCells()
    {
        for (int i = 0; i < _cellCount; i++)
        {
            int currentRow = (int)(i / _rowSize);
            int currentColumn = i % _rowSize;

            int cellY = (int)(Position.Y) + (currentRow * _cellSize) + (_cellGap * currentRow) + _padding;
            int cellX = (int)(Position.X) + (currentColumn * _cellSize) + (_cellGap * currentColumn) + _padding;

            _cells.Add(new ItemCell(cellX, cellY, _cellSize, _cellSize, new ScaledHover(1.5f)));
        }
    }

    // -- Internal Processing

    private void HoveredCell(ItemCell cell)
    {
        if (OrionHelp.RectContainsCursor(cell.Position.X, cell.Position.Y, _cellSize, _cellSize))
            cell.Hovered();
    }

    // -- Public Interface

    public void SetHoveringEffects(HoverEffect hoverEffect)
    {
        _cells.ForEach(x => x.SetHoverEffect(hoverEffect));
    }

    public void SetItem(string imageSrc, string itemId, int count)
    {
        Texture2D image = AssetManager.GetAsset<Texture2D>(imageSrc);
        int slot = count == 1 ? _cells.FindIndex(x => x.Image == null) : _cells.FindIndex(x => x.ItemId == itemId);
        string? label = count > 1 ? count.ToString() : null;

        _cells[slot].SetTo(image, itemId, label);
    }

    public void ClearItem(string itemId, int quantity)
    {
        var slot = _cells.First(x => x.ItemId == itemId);

        slot.SetTo(slot.Image, itemId, quantity.ToString());
    }

    public void Update()
    {
        _cells.ForEach(x => x.Reset());

        if (Input.Input.Pressed(Keys.I))
            Drawn = !Drawn;

        if (ContainerHovered && _cells.TrueForAll(x => !x.IsHovered))
            _cells.ForEach(x => HoveredCell(x));
    }

    public void Draw(Color? background = null, Color? cells = null, Color? border = null, Color? cellBorder = null)
    {
        if (!Drawn)
            return;

        Batcher.DrawFillRect(Position, ContainerWidth, ContainerHeight, background);

        if (border is not null)
            Batcher.DrawRect(Position, ContainerWidth, ContainerHeight, border, 1);

        _cells.Sort((s, y) => s.IsHovered.CompareTo(y.IsHovered));

        foreach (var cell in _cells)
        {
            // slots
            Batcher.DrawFillRect(cell.Position, cell.Width, cell.Height, new Color(50, 30, 30));

            // cell border
            if (cellBorder is not null)
            {
                int cellBorderSize = cell.IsHovered && cell.Image is not null ? 2 : cell.IsHovered ? 2 : 2;
                cellBorder = cell.IsHovered && cell.Image is not null ? cellBorder : cellBorder;
                Batcher.DrawRect(cell.Position, cell.Width, cell.Height, cellBorder, cellBorderSize);
            }

            // cell image
            if (cell.Image is not null)
            {
                var origin = new Vector2(cell.Image.Width / 2, cell.Image.Height / 2);
                float scale = cell.IsHovered ? (cell.Width - cell.Image.Width) / cell.Image.Width : 1f;
                Batcher.DrawTexture(cell.Image, cell.Center, origin: origin, scale: scale);
            }

            // label
            if (cell.Label is not null)
            {
                Batcher.DrawString(cell.Label, _font, new Vector2(cell.Position.X + 4, cell.Position.Y), Color.WhiteSmoke);
            }
        }
    }
}