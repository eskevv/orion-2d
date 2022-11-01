using Microsoft.Xna.Framework;
using System;

namespace OrionFramework;

public abstract class MovingObject
{
    public Vector2 Position { get; protected set; }
    public Vector2 OldPosition { get; protected set; }
    public Vector2 Velocity { get; protected set; }
    public Vector2 OldVelocity { get; protected set; }
    public AABB Bounds { get; protected set; }

    public Color CollisionIndicator { get; private set; }
    public bool PushedRightWall { get; private set; }
    public bool PushesRightWall { get; private set; }
    public bool PushedLeftWall { get; private set; }
    public bool PushesLeftWall { get; private set; }
    public bool WasOnGround { get; private set; }
    public bool OnGround { get; private set; }
    public bool WasAtCeiling { get; private set; }
    public bool AtCeiling { get; private set; }

    private World _world;

    public MovingObject(float xPos, float yPos, World world)
    {
        _world = world;
        Position = new Vector2(xPos, yPos);
    }

    protected void UpdatePhysics()
    {
        CleanUpFields();
        CheckTiles();
        UpdateOldFields();
    }

    private void CleanUpFields()
    {
        Position += Velocity * Time.DeltaTime * Environment.PixelsPerMeter;
        Bounds = new AABB(Position, Bounds.HalfSize.X * 2, Bounds.HalfSize.Y * 2);

        CollisionIndicator = Color.White;
        OnGround = false;
        AtCeiling = false;
        PushesRightWall = false;
        PushesLeftWall = false;
    }

    private void CheckTiles()
    {
        foreach (var item in _world.Colliders)
        {
            if (!item.Overlaps(Bounds))
                continue;

            CollisionIndicator = Color.MonoGameOrange;

            Console.WriteLine($" #1 {Position.Y - Bounds.HalfSize.Y} -- {item.Center.Y + item.HalfSize.Y}");

            if (Velocity.Y >= 0f && WithGround(item, out float tileTop))
            {
                Position = new Vector2(Position.X, tileTop - Bounds.HalfSize.Y);
                Velocity = new Vector2(Velocity.X, 0f);
                OnGround = true;

                Console.WriteLine("GROUND");
            }

            if (Velocity.Y < 0f && WithCeiling(item, out float tileBottom))
            {
                Position = new Vector2(Position.X, tileBottom + Bounds.HalfSize.Y);
                Velocity = new Vector2(Velocity.X, 0f);
                AtCeiling = true;
                Console.WriteLine("CEILING");
            }

            if (Velocity.X < 0f && WithLeftWall(item, out float tileRight))
            {
                Position = new Vector2(tileRight + Bounds.HalfSize.X, Position.Y);
                Velocity = new Vector2(0f, Velocity.Y);
                PushesLeftWall = true;
                Console.WriteLine("LEFT");
            }

            if (Velocity.X > 0f && WithRightWall(item, out float tileLeft))
            {
                Position = new Vector2(tileLeft - Bounds.HalfSize.X, Position.Y);
                Velocity = new Vector2(0f, Velocity.Y);
                PushesRightWall = true;
                Console.WriteLine("RIGHT");
            }
        }
    }

    private void UpdateOldFields()
    {
        Bounds = new AABB(Position, Bounds.HalfSize.X * 2, Bounds.HalfSize.Y * 2);

        OldPosition = Position;
        OldVelocity = Velocity;
        PushedRightWall = PushesRightWall;
        PushedLeftWall = PushesLeftWall;
        WasOnGround = OnGround;
        WasAtCeiling = AtCeiling;
    }

    private bool WithGround(AABB box, out float tileTop)
    {
        tileTop = box.Center.Y - box.HalfSize.Y;
        if (OldPosition.Y + Bounds.HalfSize.Y > tileTop)
            return false;

        return (MathF.Abs(box.Center.X - Bounds.Center.X) != box.HalfSize.X + Bounds.HalfSize.X);
    }

    private bool WithCeiling(AABB box, out float tileBottom)
    {
        tileBottom = box.Center.Y + box.HalfSize.Y;
        if (OldPosition.Y - Bounds.HalfSize.Y < tileBottom)
            return false;

        return (MathF.Abs(box.Center.X - Bounds.Center.X) != box.HalfSize.X + Bounds.HalfSize.X);
    }

    private bool WithLeftWall(AABB box, out float tileRight)
    {
        tileRight = box.Center.X + box.HalfSize.X;
        return OldPosition.X - Bounds.HalfSize.X >= tileRight && !AboveOrBelow(box);
    }

    private bool WithRightWall(AABB box, out float tileLeft)
    {
        tileLeft = box.Center.X - box.HalfSize.X;
        return (OldPosition.X + Bounds.HalfSize.X <= tileLeft && !AboveOrBelow(box));
    }

    private bool AboveOrBelow(AABB box)
    {
        bool directly_below = OldPosition.Y - Bounds.HalfSize.Y == box.Center.Y + box.HalfSize.Y;
        bool directly_above = OldPosition.Y + Bounds.HalfSize.Y == box.Center.Y - box.HalfSize.Y;
        return directly_above || directly_below;
    }
}