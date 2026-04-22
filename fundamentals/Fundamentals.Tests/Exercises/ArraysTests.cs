using Fundamentals.Exercises;

namespace Fundamentals.Tests.Exercises;

// Tests for the exercises in Exercises/Arrays.cs.
// These will fail until the student implements each method.
public class ArraysTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4 }, 10)]
    [InlineData(new int[0], 0)]
    [InlineData(new[] { -5, 5 }, 0)]
    [InlineData(new[] { 7 }, 7)]
    public void SumOfArray_SumsAllElements(int[] input, int expected)
        => Assert.Equal(expected, Arrays.SumOfArray(input));

    [Theory]
    [InlineData(new[] { 3, 1, 4, 1, 5, 9, 2, 6 }, 9)]
    [InlineData(new[] { -10, -20, -5 }, -5)]
    [InlineData(new[] { 42 }, 42)]
    public void FindLargest_ReturnsMax(int[] input, int expected)
        => Assert.Equal(expected, Arrays.FindLargest(input));

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 3)]
    [InlineData(new[] { 1, 3, 5 }, 0)]
    [InlineData(new[] { 2, 4, 6 }, 3)]
    [InlineData(new int[0], 0)]
    [InlineData(new[] { 0 }, 1)]
    public void CountEvens_CountsEvenNumbers(int[] input, int expected)
        => Assert.Equal(expected, Arrays.CountEvens(input));

    [Theory]
    [InlineData(new[] { 1, 2, 3 }, new[] { 3, 2, 1 })]
    [InlineData(new[] { 1 }, new[] { 1 })]
    [InlineData(new int[0], new int[0])]
    [InlineData(new[] { 1, 2, 3, 4 }, new[] { 4, 3, 2, 1 })]
    public void ReverseArray_ReturnsReversedCopy(int[] input, int[] expected)
        => Assert.Equal(expected, Arrays.ReverseArray(input));

    [Fact]
    public void ReverseArray_DoesNotMutateInput()
    {
        int[] input = { 1, 2, 3 };
        int[] snapshot = { 1, 2, 3 };
        Arrays.ReverseArray(input);
        Assert.Equal(snapshot, input); // input unchanged
    }
}
