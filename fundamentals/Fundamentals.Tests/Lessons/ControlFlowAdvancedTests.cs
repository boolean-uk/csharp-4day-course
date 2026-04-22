using Fundamentals.Lessons;

namespace Fundamentals.Tests.Lessons;

// Guards for the teaching examples in Lessons/ControlFlowAdvanced.cs.
public class ControlFlowAdvancedTests
{
    [Theory]
    [InlineData(1, "Mon")]
    [InlineData(2, "Tue")]
    [InlineData(3, "Wed")]
    [InlineData(4, "Thu")]
    [InlineData(5, "Fri")]
    [InlineData(6, "Sat")]
    [InlineData(7, "Sun")]
    [InlineData(0, "?")]
    [InlineData(99, "?")]
    public void LessonI_DayNameShort(int day, string expected)
        => Assert.Equal(expected, ControlFlowAdvanced.DayNameShort(day));

    [Theory]
    [InlineData(85, "A")]
    [InlineData(70, "A")]
    [InlineData(63, "B")]
    [InlineData(55, "C")]
    [InlineData(40, "F")]
    public void LessonJ_GradeFromMark(int mark, string expected)
        => Assert.Equal(expected, ControlFlowAdvanced.GradeFromMark(mark));

    [Theory]
    [InlineData(-5, "freezing")]
    [InlineData(0, "cold")]
    [InlineData(15, "cold")]
    [InlineData(20, "warm")]
    [InlineData(25, "warm")]
    [InlineData(30, "hot")]
    public void LessonJ_TemperatureBand(int celsius, string expected)
        => Assert.Equal(expected, ControlFlowAdvanced.TemperatureBand(celsius));
}
