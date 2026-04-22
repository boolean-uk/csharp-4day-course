using Fundamentals.Exercises;

namespace Fundamentals.Tests.Exercises;

// Tests for the exercises in Exercises/ControlFlow.cs.
// These will fail until the student implements each method.
public class ControlFlowTests
{
    [Theory]
    [InlineData(-3, "negative")]
    [InlineData(0, "zero")]
    [InlineData(7, "positive")]
    public void Sign_ClassifiesByComparisonToZero(int n, string expected)
        => Assert.Equal(expected, ControlFlow.Sign(n));

    [Theory]
    [InlineData(2024, true)]   // divisible by 4, not by 100 → leap
    [InlineData(2023, false)]  // not divisible by 4 → not leap
    [InlineData(1900, false)]  // divisible by 100 but not 400 → not leap
    [InlineData(2000, true)]   // divisible by 400 → leap
    [InlineData(2100, false)]
    public void IsLeapYear_AppliesGregorianRules(int year, bool expected)
        => Assert.Equal(expected, ControlFlow.IsLeapYear(year));

    [Theory]
    [InlineData(1, "Mon")]
    [InlineData(2, "Tue")]
    [InlineData(3, "Wed")]
    [InlineData(4, "Thu")]
    [InlineData(5, "Fri")]
    [InlineData(6, "Sat")]
    [InlineData(7, "Sun")]
    [InlineData(0, "?")]
    [InlineData(42, "?")]
    public void DayName_ReturnsShortNameOrQuestionMark(int day, string expected)
        => Assert.Equal(expected, ControlFlow.DayName(day));

    [Theory]
    [InlineData('a', true)]
    [InlineData('E', true)]
    [InlineData('i', true)]
    [InlineData('O', true)]
    [InlineData('u', true)]
    [InlineData('z', false)]
    [InlineData('Y', false)]
    [InlineData('1', false)]
    public void IsVowel_CaseInsensitive(char c, bool expected)
        => Assert.Equal(expected, ControlFlow.IsVowel(c));

    [Fact]
    public void CountPositives_CountsStrictlyPositive()
        => Assert.Equal(2, ControlFlow.CountPositives(new[] { 3, -1, 0, 7, -5 }));

    [Fact]
    public void CountPositives_Empty()
        => Assert.Equal(0, ControlFlow.CountPositives(Array.Empty<int>()));

    [Fact]
    public void CountPositives_AllNegative()
        => Assert.Equal(0, ControlFlow.CountPositives(new[] { -1, -2, -3 }));

    [Fact]
    public void FirstIndexOf_ReturnsFirstMatch()
        => Assert.Equal(1, ControlFlow.FirstIndexOf(new[] { 10, 20, 30, 20 }, 20));

    [Fact]
    public void FirstIndexOf_NotFoundReturnsMinusOne()
        => Assert.Equal(-1, ControlFlow.FirstIndexOf(new[] { 1, 2, 3 }, 9));

    [Fact]
    public void FirstIndexOf_EmptyReturnsMinusOne()
        => Assert.Equal(-1, ControlFlow.FirstIndexOf(Array.Empty<int>(), 1));

    [Fact]
    public void SumUntilNegative_StopsAtFirstNegative()
        => Assert.Equal(6, ControlFlow.SumUntilNegative(new[] { 1, 2, 3, -1, 10 }));

    [Fact]
    public void SumUntilNegative_NoNegativesSumsAll()
        => Assert.Equal(6, ControlFlow.SumUntilNegative(new[] { 1, 2, 3 }));

    [Fact]
    public void SumUntilNegative_Empty()
        => Assert.Equal(0, ControlFlow.SumUntilNegative(Array.Empty<int>()));

    [Fact]
    public void SumUntilNegative_LeadingNegative()
        => Assert.Equal(0, ControlFlow.SumUntilNegative(new[] { -5, 1, 2 }));

    [Fact]
    public void ReverseArray_ReversesElements()
        => Assert.Equal(new[] { 4, 3, 2, 1 }, ControlFlow.ReverseArray(new[] { 1, 2, 3, 4 }));

    [Fact]
    public void ReverseArray_SingleElement()
        => Assert.Equal(new[] { 7 }, ControlFlow.ReverseArray(new[] { 7 }));

    [Fact]
    public void ReverseArray_DoesNotMutateInput()
    {
        int[] input = { 1, 2, 3 };
        ControlFlow.ReverseArray(input);
        Assert.Equal(new[] { 1, 2, 3 }, input);
    }
}
