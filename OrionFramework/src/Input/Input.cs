using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OrionFramework.Input;

public static class Input
{
    // -- Properties / Fields

    private static KeyboardState _currentKeyboard;
    private static KeyboardState _prevKeyboard;
    private static MouseState _currentMouse;
    private static MouseState _previousMouse;

    public static Keys UpKey { get; set; } = Keys.Up;
    public static Keys DownKey { get; set; } = Keys.Down;
    public static Keys LeftKey { get; set; } = Keys.Left;
    public static Keys RightKey { get; set; } = Keys.Right;

    // -- Helpers

    public static Vector2 ScreenCursor => new Vector2(_currentMouse.X, _currentMouse.Y);
    public static Vector2 WorldCursor => GetWorldCursor();
    public static float RawHorizontal => GetRawHorizontal();
    public static float RawVertical => GetRawVertical();
    public static Vector2 RawAxes => GetVectorInput();

    private static float GetRawHorizontal()
    {
        float rawInput = 0;
        rawInput -= IsHeld(LeftKey) ? 1 : 0;
        rawInput += IsHeld(RightKey) ? 1 : 0;

        return rawInput;
    }

    private static float GetRawVertical()
    {
        float rawInput = 0;
        rawInput -= IsHeld(UpKey) ? 1 : 0;
        rawInput += IsHeld(DownKey) ? 1 : 0;

        return rawInput;
    }

    private static Vector2 GetVectorInput()
    {
        Vector2 axes = new Vector2(RawHorizontal, RawVertical);
        return axes == Vector2.Zero ? axes : Vector2.Normalize(axes);
    }

    private static Vector2 GetWorldCursor()
    {
        var offset = new Vector3(ScreenCursor, 0f) - (Camera.Camera.Transform.Translation);
        return new Vector2(offset.X, offset.Y) / Camera.Camera.Zoom;
    }

    // -- Initialization

    internal static void Initialize()
    {
        _currentKeyboard = Keyboard.GetState();
        _currentMouse = Mouse.GetState();
    }

    // -- Ticks

    internal static void Update()
    {
        _prevKeyboard = _currentKeyboard;
        _currentKeyboard = Keyboard.GetState();
        _previousMouse = _currentMouse;
        _currentMouse = Mouse.GetState();
    }

    // -- Keyboard

    public static bool Released(Keys key) =>
       _currentKeyboard.IsKeyUp(key) && _prevKeyboard.IsKeyDown(key);

    public static bool Pressed(Keys key) =>
       _currentKeyboard.IsKeyDown(key) && _prevKeyboard.IsKeyUp(key);

    public static bool IsHeld(Keys key) =>
       _currentKeyboard.IsKeyDown(key);

    public static bool IsUp(Keys key) =>
       _currentKeyboard.IsKeyUp(key);


    // -- Mouse

    public static bool Released(MouseButton button) =>
       GetPreviousButtonState(button) == ButtonState.Pressed && GetCurrentButtonState(button) == ButtonState.Released;

    public static bool Pressed(MouseButton button) =>
       GetPreviousButtonState(button) == ButtonState.Released && GetCurrentButtonState(button) == ButtonState.Pressed;

    public static bool IsHeld(MouseButton button) =>
       GetCurrentButtonState(button) == ButtonState.Pressed;

    public static bool IsUp(MouseButton button) =>
       GetCurrentButtonState(button) == ButtonState.Released;

    private static ButtonState GetCurrentButtonState(MouseButton button)
    {
        return button switch
        {
            MouseButton.LeftButton => _currentMouse.LeftButton,
            MouseButton.RightButton => _currentMouse.RightButton,
            MouseButton.ScrollWheel => _currentMouse.MiddleButton,
            _ => throw new NotSupportedException()
        };
    }

    private static ButtonState GetPreviousButtonState(MouseButton button)
    {
        return button switch
        {
            MouseButton.LeftButton => _previousMouse.LeftButton,
            MouseButton.RightButton => _previousMouse.RightButton,
            MouseButton.ScrollWheel => _previousMouse.MiddleButton,
            _ => throw new NotSupportedException()
        };
    }
}