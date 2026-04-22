using Fundamentals.Lessons;

namespace Fundamentals.Tests.Lessons;

// Guards for the teaching examples in Lessons/ArraysAdvanced.cs.
public class ArraysAdvancedTests
{
    // ── Multi-dimensional ──

    [Fact]
    public void Multidim_Make3x4GridHasCorrectDimensions()
    {
        int[,] grid = ArraysAdvanced.Make3x4Grid();
        Assert.Equal(3, grid.GetLength(0));
        Assert.Equal(4, grid.GetLength(1));
    }

    [Fact]
    public void Multidim_Make3x4GridSetsSeededCells()
    {
        int[,] grid = ArraysAdvanced.Make3x4Grid();
        Assert.Equal(1, grid[0, 0]);
        Assert.Equal(42, grid[1, 2]);
        Assert.Equal(0, grid[2, 3]); // untouched cells stay zero
    }

    [Fact]
    public void Multidim_GetCellReadsCorrectValue()
    {
        int[,] grid = ArraysAdvanced.GridWithInitialiser();
        Assert.Equal(6, ArraysAdvanced.GetCell(grid, 1, 2));
        Assert.Equal(1, ArraysAdvanced.GetCell(grid, 0, 0));
    }

    [Fact]
    public void Multidim_RowAndColCountsReturnDimensions()
    {
        int[,] grid = ArraysAdvanced.GridWithInitialiser();
        Assert.Equal(2, ArraysAdvanced.RowCount(grid));
        Assert.Equal(3, ArraysAdvanced.ColCount(grid));
    }

    [Fact]
    public void Multidim_SumAllCellsSumsEverything()
        => Assert.Equal(21, ArraysAdvanced.SumAllCells(ArraysAdvanced.GridWithInitialiser()));

    // ── Jagged ──

    [Fact]
    public void Jagged_MakeTriangleHasCorrectRowLengths()
    {
        int[][] tri = ArraysAdvanced.MakeTriangle();
        Assert.Equal(3, tri.Length);
        Assert.Single(tri[0]);
        Assert.Equal(2, tri[1].Length);
        Assert.Equal(3, tri[2].Length);
    }

    [Fact]
    public void Jagged_GetReadsCorrectValue()
    {
        int[][] tri = ArraysAdvanced.MakeTriangle();
        Assert.Equal(1, ArraysAdvanced.JaggedGet(tri, 0, 0));
        Assert.Equal(5, ArraysAdvanced.JaggedGet(tri, 2, 1));
    }

    [Fact]
    public void Jagged_RowLengthReturnsSpecificRowLength()
    {
        int[][] tri = ArraysAdvanced.MakeTriangle();
        Assert.Equal(1, ArraysAdvanced.RowLength(tri, 0));
        Assert.Equal(3, ArraysAdvanced.RowLength(tri, 2));
    }

    [Fact]
    public void Jagged_SumAllJaggedSumsEverything()
        => Assert.Equal(21, ArraysAdvanced.SumAllJagged(ArraysAdvanced.MakeTriangle()));
}
