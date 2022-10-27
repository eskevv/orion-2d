using Microsoft.Xna.Framework;
using System;
namespace OrionFramework;

public static class GameMath
{
    public static Random _random = new();

    #region Vector2Rotations

    /// <summary>Rotate a Vector by a certain amount around another point.</summary>
    public static void Rotate(this ref Vector2 item, Vector2 pointOfRotation, float amount)
    {
        Vector2 direction = pointOfRotation - item;
        float distance = direction.Length();

        float angle = MathF.Atan2(direction.Y, direction.X);
        item.X = MathF.Cos(angle + amount) * distance + pointOfRotation.X;
        item.Y = MathF.Sin(angle + amount) * distance + pointOfRotation.Y;
    }

    /// <summary>Get an angle from a direction using Atan2. The y-axis is not flipped.</summary>
    public static float ToAngle(this Vector2 vector) =>
        MathF.Atan2(vector.Y, vector.X);

    #endregion

    #region Direction

    /// <summary>Get the normalized direction to another point.</summary>
    public static Vector2 NormalDirectionTo(this Vector2 item, Vector2 point)
    {
        Vector2 direction = point - item;
        return direction != Vector2.Zero ? Vector2.Normalize(direction) : direction;
    }

    /// <summary>Get the formed angle between this vector and another point.</summary>
    public static float AngleToPoint(this Vector2 item, Vector2 point)
    {
        var direction = point - item;
        return MathF.Atan2(direction.Y, direction.X);
    }

    /// <summary>Get the normalized direction from raw keyboard input.</summary>
    public static Vector2 NormalInputDirection()
    {
        var direction = new Vector2(Input.RawHorizontal, Input.RawVertical);

        if (direction.LengthSquared() > 1)
            direction.Normalize();

        return direction;
    }

    /// <summary>Create a speed vector from an angle and magnitude.</summary>
    public static Vector2 FromPolar(float angle, float magnitude) =>
        magnitude * new Vector2(MathF.Cos(angle), MathF.Sin(angle));

    #endregion

    #region RandomNumbers

    public static int NextInt(int value) =>
        _random.Next(value);

    public static float RandomFloat(float min, float max) =>
        (float)(_random.NextDouble() * (max - min)) + min;

    public static float NextFloat() =>
        (float)(_random.NextDouble());

    #endregion
}