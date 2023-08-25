using System.Numerics;

namespace Geometry.Core;

public class Rectangle2
{
    public Rectangle2(Point a, Point b, Point c, Point d)
    {
        this.A = a;
        this.B = b;
        this.C = c;
        this.D = d;
    }

    public Point A { get; }

    public Point B { get; }

    public Point C { get; }

    public Point D { get; }

    public bool Contains(Point point)
    {
        var t1 = new Triangle(ToVector(this.A), ToVector(this.B), ToVector(this.C));
        var t2 = new Triangle(ToVector(this.A), ToVector(this.C), ToVector(this.D));

        Vector2 p = ToVector(point);
        return t1.Contains(p) || t2.Contains(p);
    }

    private static Vector2 ToVector(Point p) => new((float)p.X, (float)p.Y);
}
