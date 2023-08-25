using System.Numerics;

using static Geometry.Core.Vector2Utils;

namespace Geometry.Core;

public class Triangle
{
    public Triangle(Vector2 a, Vector2 b, Vector2 c)
    {
        this.A = a;
        this.B = b;
        this.C = c;
    }

    public Vector2 A { get; }

    public Vector2 B { get; }

    public Vector2 C { get; }

    /// <summary>
    /// Determines whether a point falls into the triangle.
    /// Source: <see cref="https://math.stackexchange.com/a/51459"/>
    /// </summary>
    /// <param name="point"></param>
    /// <returns><c>true</c> if <paramref name="point"/> is inside of the triangle, <c>false</c> otherwise.</returns>
    public bool Contains(Vector2 point)
    {
        float d = Cross(this.A, this.B) + Cross(this.B, this.C) + Cross(this.C, A);

        float weightA = Weight(point, d, this.B, this.C);
        float weightB = Weight(point, d, this.C, this.A);
        float weightC = Weight(point, d, this.A, this.B);

        return IsInsideZeroOne(weightA) && IsInsideZeroOne(weightB) && IsInsideZeroOne(weightC);
    }

    private static bool IsInsideZeroOne(float value) => value >= 0 && value <= 1;

    private static float Weight(Vector2 point, float d, Vector2 p1, Vector2 p2)
    {
        return (Cross(p1, p2) + Cross(point, p1 - p2)) / d;
    }
}
