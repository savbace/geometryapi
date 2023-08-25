using System.Numerics;

namespace Geometry.Core;

public static class Vector2Utils
{
    public static float Cross(Vector2 value1, Vector2 value2) => value1.X * value2.Y - value1.Y * value2.X;
}