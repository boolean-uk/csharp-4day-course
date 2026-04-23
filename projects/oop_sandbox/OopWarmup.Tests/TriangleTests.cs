using OopWarmup;

namespace OopWarmup.Tests;

public class TriangleTests
{
    // Classic 3-4-5 right triangle — easy to verify by hand.
    private static Triangle RightTriangle()
        => new Triangle(new Point(0, 0), new Point(3, 0), new Point(0, 4));

    [Fact]
    public void Perimeter_SumsTheThreeSideLengths()
    {
        // Sides: 3, 4, 5 → perimeter 12
        Assert.Equal(12, RightTriangle().Perimeter, precision: 9);
    }

    [Fact]
    public void Area_OfRightTriangle_UsesHeronsFormula()
    {
        // Sides 3-4-5: s = 6, area = √(6·3·2·1) = √36 = 6
        Assert.Equal(6, RightTriangle().Area, precision: 9);
    }

    [Fact]
    public void Area_OfEquilateralTriangle()
    {
        // Unit-sided equilateral → area = √3 / 4 ≈ 0.4330127...
        Triangle t = new Triangle(
            new Point(0, 0),
            new Point(1, 0),
            new Point(0.5, Math.Sqrt(3) / 2));
        Assert.Equal(Math.Sqrt(3) / 4, t.Area, precision: 6);
    }

    [Fact]
    public void Triangle_IsA_Shape()
    {
        Assert.IsAssignableFrom<Shape>(RightTriangle());
    }

    [Fact]
    public void Describe_IncludesTypeNameAndFormattedArea()
    {
        // Triangle inherits the default Shape.Describe, which uses
        // GetType().Name and formats Area with "F2".
        string describe = RightTriangle().Describe();
        Assert.Contains("Triangle", describe);
        Assert.Contains("6.00", describe);
    }
}
