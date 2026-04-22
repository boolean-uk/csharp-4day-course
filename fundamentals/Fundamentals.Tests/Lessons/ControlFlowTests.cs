using Fundamentals.Lessons;

namespace Fundamentals.Tests.Lessons;

// Guards for the teaching examples in Lessons/ControlFlow.cs.
// These should always pass — they protect the lesson content from accidental "fixes".
public class ControlFlowTests
{
    [Theory]
    [InlineData(-3, "negative")]
    [InlineData(0, "zero")]
    [InlineData(7, "positive")]
    public void LessonA_DescribeSign(int n, string expected)
        => Assert.Equal(expected, ControlFlow.DescribeSign(n));

    // The gotcha DELIBERATELY adds b += 10 outside the if (no braces).
    // These assertions pin that behaviour so a student can't "fix" the lesson file.
    [Fact]
    public void LessonB_NoBracesGotcha_RunsSecondLineEvenWhenConditionFalse()
        => Assert.Equal(10, ControlFlow.NoBracesGotcha(0, 0));

    [Fact]
    public void LessonB_NoBracesGotcha_RunsBothLinesWhenConditionTrue()
        => Assert.Equal(11, ControlFlow.NoBracesGotcha(1, 0));

    [Fact]
    public void LessonB_NoBracesFixed_SkipsBothLinesWhenConditionFalse()
        => Assert.Equal(0, ControlFlow.NoBracesFixed(0, 0));

    [Fact]
    public void LessonB_NoBracesFixed_RunsBothLinesWhenConditionTrue()
        => Assert.Equal(11, ControlFlow.NoBracesFixed(1, 0));

    [Theory]
    [InlineData(85, "A")]
    [InlineData(65, "B")]
    [InlineData(55, "C")]
    [InlineData(40, "F")]
    [InlineData(70, "A")]   // boundary
    [InlineData(60, "B")]   // boundary
    [InlineData(50, "C")]   // boundary
    public void LessonC_Grade(int mark, string expected)
        => Assert.Equal(expected, ControlFlow.Grade(mark));

    [Theory]
    [InlineData(1, "weekday")]
    [InlineData(5, "weekday")]
    [InlineData(6, "weekend")]
    [InlineData(7, "weekend")]
    [InlineData(0, "unknown")]
    [InlineData(99, "unknown")]
    public void LessonD_DayKind(int day, string expected)
        => Assert.Equal(expected, ControlFlow.DayKind(day));

    [Theory]
    [InlineData(4, "even")]
    [InlineData(7, "odd")]
    [InlineData(0, "even")]
    public void LessonE_Parity(int n, string expected)
        => Assert.Equal(expected, ControlFlow.Parity(n));

    [Theory]
    [InlineData(8, 4)]
    [InlineData(1, 1)]
    [InlineData(0, 0)]
    [InlineData(16, 5)]
    public void LessonF_CountHalvings(int n, int expected)
        => Assert.Equal(expected, ControlFlow.CountHalvings(n));

    [Theory]
    [InlineData(9, 9)]
    [InlineData(10, 12)]
    [InlineData(13, 15)]
    public void LessonF_FirstMultipleOfThree(int start, int expected)
        => Assert.Equal(expected, ControlFlow.FirstMultipleOfThree(start));

    [Fact]
    public void LessonG_SumArray()
        => Assert.Equal(10, ControlFlow.SumArray(new[] { 1, 2, 3, 4 }));

    [Fact]
    public void LessonG_SumArray_Empty()
        => Assert.Equal(0, ControlFlow.SumArray(Array.Empty<int>()));

    [Fact]
    public void LessonH_MaxValue()
        => Assert.Equal(9, ControlFlow.MaxValue(new[] { 3, 7, 1, 9, 4 }));

    [Fact]
    public void LessonH_MaxValue_SingleElement()
        => Assert.Equal(5, ControlFlow.MaxValue(new[] { 5 }));
}
