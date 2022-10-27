using Microsoft.Xna.Framework;
namespace OrionFramework;

public static class Collision
{
    public static bool BoxContainsCursor(float left, float top, float width, float height)
    {
        Vector2 cursor = Input.ScreenCursor;
        bool containsX = cursor.X >= left && cursor.X <= left + width;
        bool containsY = cursor.Y >= top && cursor.Y <= top + height;

        return containsX && containsY;
    }

    public static bool BoxContainsCursor(Rectangle rect)
    {
        Vector2 cursor = Input.ScreenCursor;
        bool containsX = cursor.X >= rect.X && cursor.X <= rect.X + rect.Width;
        bool containsY = cursor.Y >= rect.Y && cursor.Y <= rect.Y + rect.Height;

        return containsX && containsY;
    }
}