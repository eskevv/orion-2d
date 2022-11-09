using Microsoft.Xna.Framework;

namespace OrionFramework.Editor;

public class Square
{
    public string Name;
    public int X;
    public int Y;
    public int Width;
    public int Height;
    public Color Tint;

    public Square(string name, int x, int y, int width, int height, Color tint)
    {
        Name = name;
        X = x;
        Y = y;
        Width = width;
        Height = height;
        Tint = tint;
    }
}