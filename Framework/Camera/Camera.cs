using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Orion;

public static class Camera
{
    // -- Properties / Fields

    private static GraphicsDevice _device = null!;
    private static int _xMin;
    private static int _xMax;
    private static int _yMin;
    private static int _yMax;
    private static float _zoom;
    private static Vector2 _position;

    public static float Rotation { get; set; }

    public static float Zoom
    {
        get => _zoom;
        set => _zoom = MathF.Max(0.1f, value);
    }

    public static Vector2 Position
    {
        get => _position;
        set => Restrict(value, _device.Viewport);
    }

    public static Matrix Transform => GetTransform();
    
    // -- Internal Processing

    private static Matrix GetTransform()
    {
        Viewport vp = _device.Viewport;
        return Matrix.CreateTranslation(-Position.X, -Position.Y, 0f) *
           Matrix.CreateRotationZ(Rotation) *
           Matrix.CreateScale(new Vector3(_zoom, _zoom, 1)) *
           Matrix.CreateTranslation(vp.Width * 0.5f, vp.Height * 0.5f, 0f);
    }

    private static void Restrict(Vector2 position, Viewport vp)
    {
        _position = position;

        if (_position.X < _xMin + (vp.Width / 2) / Zoom)
            _position = new Vector2(_xMin + (vp.Width / 2) / Zoom, _position.Y);
        else if (_position.X > _xMax - (vp.Width / 2) / Zoom)
            _position = new Vector2(_xMax - (vp.Width / 2) / Zoom, _position.Y);

        if (_position.Y < _yMin + (vp.Height / 2) / Zoom)
            _position = new Vector2(_position.X, _yMin + (vp.Height / 2) / Zoom);
        else if (_position.Y > _yMax - (vp.Height / 2) / Zoom)
            _position = new Vector2(_position.X, _yMax - (vp.Height / 2) / Zoom);
    }

    // -- Public Interface

    internal static void Initialize(GraphicsDevice device)
    {
        _device = device;
        _zoom = 1f;
        _xMax = device.Viewport.Width;
        _yMax = device.Viewport.Height;
    }

    public static void SetLimits(int xMin, int xMax, int yMin, int yMax)
    {
        _xMin = xMin;
        _xMax = xMax;
        _yMin = yMin;
        _yMax = yMax;
    }
}