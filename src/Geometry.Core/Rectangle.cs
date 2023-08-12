namespace Geometry.Core;

public class Rectangle
{
    public Rectangle(Point location, double width, double height)
        : this(location.X, location.Y, width, height)
    {
    }

    public Rectangle(double x, double y, double width, double height)
    {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
    }

    public double X { get; }

    public double Y { get; }

    public double Width { get; }

    public double Height { get; }

    public bool Contains(Point point) => this.Contains(point.X, point.Y);

    public bool Contains(double x, double y) =>
        this.X <= x
            && x < this.X + this.Width
            && this.Y <= y
            && y < this.Y + this.Height;
}

public record Point(double X, double Y);
