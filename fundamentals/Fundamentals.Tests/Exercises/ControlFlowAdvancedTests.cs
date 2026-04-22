using Fundamentals.Exercises;

namespace Fundamentals.Tests.Exercises;

// Tests for the exercises in Exercises/ControlFlowAdvanced.cs.
// These will fail until the student implements each method.
public class ControlFlowAdvancedTests
{
    [Theory]
    [InlineData(85, "A")]
    [InlineData(70, "A")]
    [InlineData(69, "B")]
    [InlineData(60, "B")]
    [InlineData(55, "C")]
    [InlineData(50, "C")]
    [InlineData(49, "F")]
    [InlineData(0, "F")]
    public void GradeFromMark_ReturnsLetterGrade(int mark, string expected)
        => Assert.Equal(expected, ControlFlowAdvanced.GradeFromMark(mark));

    [Theory]
    [InlineData("red", "stop")]
    [InlineData("amber", "prepare")]
    [InlineData("yellow", "prepare")]
    [InlineData("green", "go")]
    [InlineData("purple", "?")]
    [InlineData("", "?")]
    public void TrafficLightAction_ReturnsAction(string colour, string expected)
        => Assert.Equal(expected, ControlFlowAdvanced.TrafficLightAction(colour));
}
