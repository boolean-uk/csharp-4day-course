using Fundamentals.Exercises;

namespace Fundamentals.Tests.Exercises;

// Tests for the advanced exercises in Exercises/ArraysAdvanced.cs.
public class ArraysAdvancedTests
{
    // ── Transpose ──

    [Fact]
    public void Transpose_SwapsRowsAndColumns()
    {
        int[,] input = { { 1, 2, 3 }, { 4, 5, 6 } };      // 2×3
        int[,] result = ArraysAdvanced.Transpose(input);

        Assert.Equal(3, result.GetLength(0)); // rows swapped with cols
        Assert.Equal(2, result.GetLength(1));

        Assert.Equal(1, result[0, 0]);
        Assert.Equal(4, result[0, 1]);
        Assert.Equal(2, result[1, 0]);
        Assert.Equal(5, result[1, 1]);
        Assert.Equal(3, result[2, 0]);
        Assert.Equal(6, result[2, 1]);
    }

    [Fact]
    public void Transpose_OfSquareMatrixStillSwapsOffDiagonal()
    {
        int[,] input = { { 1, 2 }, { 3, 4 } };            // 2×2
        int[,] result = ArraysAdvanced.Transpose(input);

        Assert.Equal(1, result[0, 0]);
        Assert.Equal(3, result[0, 1]);
        Assert.Equal(2, result[1, 0]);
        Assert.Equal(4, result[1, 1]);
    }

    [Fact]
    public void Transpose_OfSingleRowProducesSingleColumn()
    {
        int[,] input = { { 1, 2, 3 } };                   // 1×3
        int[,] result = ArraysAdvanced.Transpose(input);

        Assert.Equal(3, result.GetLength(0));
        Assert.Equal(1, result.GetLength(1));
        Assert.Equal(1, result[0, 0]);
        Assert.Equal(2, result[1, 0]);
        Assert.Equal(3, result[2, 0]);
    }

    // ── HasDuplicates ──

    [Theory]
    [InlineData(new[] { 1, 2, 3 }, false)]
    [InlineData(new[] { 1, 2, 1 }, true)]
    [InlineData(new int[0], false)]
    [InlineData(new[] { 5 }, false)]
    [InlineData(new[] { 1, 1 }, true)]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 }, true)]
    public void HasDuplicates_DetectsRepeatedValues(int[] input, bool expected)
        => Assert.Equal(expected, ArraysAdvanced.HasDuplicates(input));
}
