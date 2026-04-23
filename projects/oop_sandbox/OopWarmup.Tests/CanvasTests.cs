using OopWarmup;

namespace OopWarmup.Tests;

public class CanvasTests
{
    [Fact]
    public void FindLargest_OnEmptyCanvas_ReturnsNull()
    {
        Canvas c = new Canvas();
        Assert.Null(c.FindLargest());
    }

    [Fact]
    public void FindLargest_WithOneShape_ReturnsIt()
    {
        Canvas c = new Canvas();
        Circle only = new Circle(new Point(0, 0), 5);
        c.Add(only);
        Assert.Same(only, c.FindLargest());
    }

    [Fact]
    public void FindLargest_PicksShapeWithGreatestArea()
    {
        Canvas c = new Canvas();
        Shape smallCircle   = new Circle(new Point(0, 0), 1);                                         // area π ≈ 3.14
        Shape bigRectangle  = new Rectangle(new Point(0, 0), 10, 10);                                 // area 100
        Shape mediumTriangle = new Triangle(new Point(0, 0), new Point(3, 0), new Point(0, 4));      // area 6

        c.Add(smallCircle);
        c.Add(bigRectangle);
        c.Add(mediumTriangle);

        // Assert.Same — return the same reference, not a copy.
        Assert.Same(bigRectangle, c.FindLargest());
    }

    [Fact]
    public void FindLargest_WithTwoEqualAreas_ReturnsOneOfThem()
    {
        Canvas c = new Canvas();
        Shape a = new Rectangle(new Point(0, 0), 5, 5);   // area 25
        Shape b = new Rectangle(new Point(10, 10), 5, 5); // area 25
        c.Add(a);
        c.Add(b);

        Shape? largest = c.FindLargest();
        Assert.True(largest == a || largest == b);
    }
}
