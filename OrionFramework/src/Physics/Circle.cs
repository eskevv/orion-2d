namespace OrionFramework.Physics;

public struct Circle
{
    public int X { get; set; }
    public int Y { get; set; }
    public float Radius { get; set; }

    public Circle(int x, int y, float radius)
    {
        X = x;
        Y = y;
        Radius = radius;
    }

    public bool Intersects(Circle c)
    {
        float totalDistance = (Radius + c.Radius) * (Radius + c.Radius);
        float xSquared = (c.X - X) * (c.X - X);
        float ySquared = (c.Y - Y) * (c.Y - Y);

        return xSquared + ySquared <= totalDistance;
    }
}