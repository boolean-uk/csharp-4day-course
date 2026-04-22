namespace Fundamentals.Lessons;

// Theme: Arrays (advanced) — multi-dimensional arrays and jagged arrays.
// Tackle this AFTER you've finished the core Arrays lesson.
//
// Two very different things with confusingly-similar syntax:
//   int[,] grid     — RECTANGULAR (multi-dimensional) array, one block of memory
//   int[][] jagged  — JAGGED (array of arrays), each row independent
//
// The matching exercises (Transpose, HasDuplicates) live in
// Exercises/ArraysAdvanced.cs.
public static class ArraysAdvanced
{
    // ─────────────────────────────────────────────────────────────
    // LESSON H.1: Multi-dimensional (rectangular) arrays — int[,]
    // ─────────────────────────────────────────────────────────────
    // Declared with a comma inside the brackets. Every "row" has the same
    // length — the whole thing is a single rectangular block of memory.
    // Indexed with ONE pair of brackets containing TWO indices: grid[row, col]
    //
    // Use when your data is naturally rectangular: a chess board, a matrix,
    // pixels in an image of fixed size.

    public static int[,] Make3x4Grid()
    {
        // 3 rows × 4 columns, all zeros by default.
        // Set one cell so we can show mutation works the same way as 1D arrays.
        int[,] grid = new int[3, 4];
        grid[0, 0] = 1;
        grid[1, 2] = 42;
        return grid;
    }

    // Initialiser syntax works too — each inner { ... } is a row.
    public static int[,] GridWithInitialiser()
    {
        // 2 rows × 3 columns:
        //   row 0:  1 2 3
        //   row 1:  4 5 6
        return new int[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };
    }

    // Indexing: grid[row, col] — ONE pair of brackets, TWO indices.
    public static int GetCell(int[,] grid, int row, int col)
    {
        // e.g. grid = { {1,2,3}, {4,5,6} }, row = 1, col = 2 → returns 6
        return grid[row, col];
    }

    // On a multi-dim array .Length gives you TOTAL cells. To get the size
    // along one axis, use .GetLength(dimension):
    //   GetLength(0) → number of rows
    //   GetLength(1) → number of columns
    public static int RowCount(int[,] grid)
    {
        return grid.GetLength(0);
    }

    public static int ColCount(int[,] grid)
    {
        return grid.GetLength(1);
    }

    // Iteration — nested loops, one per dimension:
    public static int SumAllCells(int[,] grid)
    {
        // e.g. grid = { {1,2,3}, {4,5,6} }; returns 21
        int total = 0;
        for (int r = 0; r < grid.GetLength(0); r++)
        {
            for (int c = 0; c < grid.GetLength(1); c++)
            {
                total += grid[r, c];
            }
        }
        return total;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON H.2: Jagged arrays — int[][]
    // ─────────────────────────────────────────────────────────────
    // An "array of arrays". The outer array has a length; each inner array
    // is its own independent array that you create separately. Rows can be
    // DIFFERENT LENGTHS — that's the "jagged" bit.
    //
    // Declared and indexed with TWO sets of brackets: jagged[row][col].
    //
    // Use when rows genuinely aren't the same shape — a triangle of numbers,
    // rows where each represents a record of different size, etc.

    public static int[][] MakeTriangle()
    {
        // Outer array has 3 slots.
        // Each inner array is created separately, with its own length.
        //   row 0:  1
        //   row 1:  2 3
        //   row 2:  4 5 6
        int[][] triangle = new int[3][];
        triangle[0] = new int[] { 1 };
        triangle[1] = new int[] { 2, 3 };
        triangle[2] = new int[] { 4, 5, 6 };
        return triangle;
    }

    // Indexing uses TWO brackets — not one-with-comma like multi-dim arrays.
    public static int JaggedGet(int[][] jagged, int row, int col)
    {
        // e.g. triangle[2][1] → returns 5
        return jagged[row][col];
    }

    // Because each row is a regular 1D array, .Length works as normal on it.
    public static int RowLength(int[][] jagged, int row)
    {
        // e.g. triangle, row = 2 → returns 3
        return jagged[row].Length;
    }

    // Iterating a jagged array — outer loop uses jagged.Length,
    // inner loop uses jagged[r].Length (each row may be a different size).
    public static int SumAllJagged(int[][] jagged)
    {
        // e.g. triangle → returns 1 + 2 + 3 + 4 + 5 + 6 = 21
        int total = 0;
        for (int r = 0; r < jagged.Length; r++)
        {
            for (int c = 0; c < jagged[r].Length; c++)
            {
                total += jagged[r][c];
            }
        }
        return total;
    }

    // ─────────────────────────────────────────────────────────────
    // Summary — which to use
    // ─────────────────────────────────────────────────────────────
    // Rectangular data, fixed shape        → int[,]     (multi-dim)
    // Rows of different lengths            → int[][]    (jagged)
    //
    // Multi-dim is more memory-efficient (one block) and feels natural for
    // matrices. Jagged is more flexible because each row is independent.
}
