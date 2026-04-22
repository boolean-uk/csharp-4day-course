using Fundamentals.Exercises;

namespace Fundamentals.Tests;

public class NumbersTests
{
    // ── Lesson guards — protect the teaching examples from accidental "fixes" ──

    [Fact]
    public void LessonA_DoubleMultipliesByTwo()
        => Assert.Equal(42, Numbers.Double(21));

    [Fact]
    public void LessonB_CountUpToFiveReturnsFive()
        => Assert.Equal(5, Numbers.CountUpToFive());

    [Fact]
    public void LessonD_IntegerDivisionTruncates()
        => Assert.Equal(3, Numbers.DivideIntegers(7, 2));

    [Fact]
    public void LessonE_DoubleToIntTruncates()
        => Assert.Equal(3, Numbers.DoubleToInt(3.9));

    [Fact]
    public void LessonE_CastingFixesTruncation()
        => Assert.Equal(3.5, Numbers.DivideWithCasting(7, 2));

    // ── Exercises ──

    [Theory]
    [InlineData(3, 4, 7)]
    [InlineData(-1, 1, 0)]
    [InlineData(0, 0, 0)]
    public void Add_ReturnsSum(int a, int b, int expected)
        => Assert.Equal(expected, Numbers.Add(a, b));

    [Theory]
    [InlineData(3.0, 4.0, 12.0)]
    [InlineData(2.5, 4.0, 10.0)]
    public void RectangleArea_ReturnsWidthTimesHeight(double w, double h, double expected)
        => Assert.Equal(expected, Numbers.RectangleArea(w, h));

    [Theory]
    [InlineData(0, 32)]
    [InlineData(100, 212)]
    [InlineData(-40, -40)]
    public void CelsiusToFahrenheit_ConvertsCorrectly(double c, double expected)
        => Assert.Equal(expected, Numbers.CelsiusToFahrenheit(c), precision: 6);
}
