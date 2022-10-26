using Microsoft.Xna.Framework;
using System;

namespace Orion;

public struct AABB
{
    public Vector2 Center;
    public Vector2 HalfSize;

    public Rectangle AsRectangle => new Rectangle((int)(Center.X - HalfSize.X), (int)(Center.Y - HalfSize.Y), (int)(HalfSize.X * 2), (int)(HalfSize.Y * 2));

    public AABB(Vector2 center, float w, float h)
    {
        Center = center;
        HalfSize = new Vector2(w / 2, h / 2);
    }

    public bool Overlaps(AABB other)
    {
        if (MathF.Abs(Center.X - other.Center.X) > HalfSize.X + other.HalfSize.X) 
            return false;
        
        if (MathF.Abs(Center.Y - other.Center.Y) > HalfSize.Y + other.HalfSize.Y) 
            return false;
        
        return true;
    }

    public override string ToString() => 
        $"[X: {Center.X - HalfSize.X}, Y: {Center.Y - HalfSize.Y}], HalfSize: {HalfSize}";
}