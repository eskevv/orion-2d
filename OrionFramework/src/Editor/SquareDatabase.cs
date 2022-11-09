using Microsoft.Xna.Framework;

namespace OrionFramework.Editor;

public static class SquareDatabase
{
    public static Square[] Squares = CreateSquares();

    private static Square[] CreateSquares()
    {
        return new[]
        {
            new Square("Red", 20, 400, 40, 40, Color.Aqua),
            new Square("Blue", 100, 19, 40, 40, Color.Red),
            new Square("Black", 200, 101, 40, 40, Color.Green),
        };
    }
}