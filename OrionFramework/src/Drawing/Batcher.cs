using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrionFramework;

public static class Batcher
{
    // -- Properties / Fields

    private static ShapeBatch _shapeBatcher = null!;
    private static SpriteBatch _spriteBatcher = null!;

    private static Matrix? _transform;
    private static Color _defaultColor = Color.WhiteSmoke;

    private static bool _addedShape;
    private static bool _addedTexture;

    // -- Initialization

    internal static void Initialize(ShapeBatch shapeBatch, SpriteBatch spriteBatch)
    {
        _shapeBatcher = shapeBatch;
        _spriteBatcher = spriteBatch;
    }

    // -- Internal Processing

    private static void FlushShapes()
    {
        _shapeBatcher.End();
        _shapeBatcher.Begin(transformMatrix: _transform);
        _addedShape = false;
    }

    private static void FlushTextures()
    {
        _spriteBatcher.End();
        _spriteBatcher.Begin(transformMatrix: _transform, samplerState: SamplerState.PointClamp);
        _addedTexture = false;
    }

    // -- Public Interface

    public static void DrawFillRect(Vector2 position, float width, float height, Color? color = null)
    {
        if (_addedTexture)
            FlushTextures();

        Color fillColor = color ?? _defaultColor;
        _shapeBatcher.DrawFillRect(position.X, position.Y, width, height, fillColor);
        _addedShape = true;
    }

    public static void DrawFillRect(Rectangle rect, Color? color = null)
    {
        if (_addedTexture)
            FlushTextures();

        Color fillColor = color ?? _defaultColor;
        _shapeBatcher.DrawFillRect(rect.X, rect.Y, rect.Width, rect.Height, fillColor);
        _addedShape = true;
    }

    public static void DrawRect(Vector2 position, float width, float height, Color? color = null, int thickness = 1, float rotation = 0f)
    {
        if (_addedTexture)
            FlushTextures();

        Color fillColor = color ?? _defaultColor;
        _shapeBatcher.DrawRect(position.X, position.Y, width, height, fillColor, thickness, rotation);
        _addedShape = true;
    }

    public static void DrawRect(Rectangle rect, Color? color = null, int thickness = 1, float rotation = 0f)
    {
        if (_addedTexture)
            FlushTextures();

        Color fillColor = color ?? _defaultColor;
        _shapeBatcher.DrawRect(rect.X, rect.Y, rect.Width, rect.Height, fillColor, thickness, rotation);
        _addedShape = true;
    }

    public static void DrawLine(int x1, int y1, int x2, int y2, Color? color = null, int thickness = 1)
    {
        if (_addedTexture)
            FlushTextures();

        Color fillColor = color ?? _defaultColor;
        _shapeBatcher.DrawLine(x1, y1, x2, y2, fillColor, thickness);
        _addedShape = true;
    }

    public static void DrawCircle(int x, int y, float radius, Color? color = null, int thickness = 1)
    {
        if (_addedTexture)
            FlushTextures();

        Color fillColor = color ?? _defaultColor;
        _shapeBatcher.DrawCircle(x, y, radius, fillColor, thickness);
        _addedShape = true;
    }

    public static void DrawTexture(Texture2D texture, Vector2 position, Rectangle? sourceRect = null, Color? color = null, float rotation = 0f, Vector2? origin = null,
        float scale = 1f, SpriteEffects effect = 0)
    {
        if (_addedShape)
            FlushShapes();

        Color drawColor = color ?? Color.White;
        Vector2 drawOrigin = origin ?? Vector2.Zero;
        _spriteBatcher.Draw(texture, position, sourceRect, drawColor, rotation, drawOrigin, scale, effect, 0f);
        _addedTexture = true;
    }

    public static void DrawString(string text, SpriteFont font, Vector2 position, Color color, float rotation = 0f, Vector2? origin = null,
        float scale = 1f, SpriteEffects effect = 0)
    {
        if (_addedShape)
            FlushShapes();

        Vector2 drawOrigin = origin ?? Vector2.Zero;
        _spriteBatcher.DrawString(font, text, position, color, rotation, drawOrigin, scale, effect, 0f);
        _addedTexture = true;
    }

    public static void Begin(Matrix? transform = null)
    {
        _transform = transform;
        _shapeBatcher.Begin(transform, null);
        _spriteBatcher.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transform);
    }

    public static void Present()
    {
        _shapeBatcher.End();
        _spriteBatcher.End();
    }
}