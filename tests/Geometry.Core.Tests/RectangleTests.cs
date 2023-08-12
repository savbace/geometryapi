namespace Geometry.Core.Tests;

public class RectangleTests
{
    [Fact]
    public void Contains_PointInside_True()
    {
        var sut = new Rectangle(1.5, 2, 10, 5);
        bool actual = sut.Contains(2, 3);
        Assert.True(actual);
    }

    [Fact]
    public void Contains_PointOutside_False()
    {
        var sut = new Rectangle(new Point(1.5, 2), 10, 5);
        bool actual = sut.Contains(new Point(0, 0));
        Assert.False(actual);
    }

    [Fact]
    public void Contains_PointOnEdge_True()
    {
        var sut = new Rectangle(1.5, 2, 10, 5);
        bool actual = sut.Contains(3, 2);
        Assert.True(actual);
    }

    [Fact]
    public void Contains_PointOnVertex_True()
    {
        var sut = new Rectangle(1.5, 2, 10, 5);
        bool actual = sut.Contains(1.5, 2);
        Assert.True(actual);
    }
}
