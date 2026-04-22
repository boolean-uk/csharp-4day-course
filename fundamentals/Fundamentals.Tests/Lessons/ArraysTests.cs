using Fundamentals.Lessons;

namespace Fundamentals.Tests.Lessons;

// Guards for the teaching examples in Lessons/Arrays.cs.
public class ArraysTests
{
    // ── Lesson A: declarations ──

    [Fact]
    public void LessonA_EmptyWithSizeHasLength5()
        => Assert.Equal(5, Arrays.EmptyWithSize().Length);

    [Fact]
    public void LessonA_AllFourFormsProduceSameContent()
    {
        Assert.Equal(Arrays.LonghandInitialiser(), Arrays.ShorthandInitialiser());
        Assert.Equal(Arrays.LonghandInitialiser(), Arrays.CollectionExpression());
    }

    // ── Lesson C: default values ──

    [Fact]
    public void LessonC_DefaultIntsAreZero()
        => Assert.Equal(new[] { 0, 0, 0, 0, 0 }, Arrays.DefaultInts());

    [Fact]
    public void LessonC_DefaultBoolsAreFalse()
        => Assert.Equal(new[] { false, false, false }, Arrays.DefaultBools());

    [Fact]
    public void LessonC_DefaultStringsAreNull()
    {
        string?[] defaults = Arrays.DefaultStrings();
        Assert.All(defaults, element => Assert.Null(element));
    }

    // ── Lesson D: indexing, Length, mutation ──

    [Fact]
    public void LessonD_FirstLastAndLengthRead()
    {
        int[] arr = { 10, 20, 30 };
        Assert.Equal(10, Arrays.FirstElement(arr));
        Assert.Equal(30, Arrays.LastElement(arr));
        Assert.Equal(3, Arrays.HowMany(arr));
    }

    [Fact]
    public void LessonD_SetFirstMutatesCallersArray()
    {
        int[] arr = { 1, 2, 3 };
        Arrays.SetFirst(arr, 99);
        Assert.Equal(99, arr[0]);
        Assert.Equal(2, arr[1]); // rest untouched
    }

    // ── Lesson E: out of bounds ──

    [Fact]
    public void LessonE_AccessOutOfBoundsThrows()
        => Assert.Throws<IndexOutOfRangeException>(() => Arrays.AccessOutOfBounds(new[] { 1, 2, 3 }));

    [Theory]
    [InlineData(0, 10)]
    [InlineData(2, 30)]
    [InlineData(10, -1)]   // out of range, fallback
    [InlineData(-1, -1)]   // negative, fallback
    public void LessonE_SafeGetHandlesBothInAndOutOfRange(int index, int expected)
        => Assert.Equal(expected, Arrays.SafeGet(new[] { 10, 20, 30 }, index, -1));

    // ── Lesson F: iteration ──

    [Fact]
    public void LessonF_ForeachAndForProduceSameSum()
    {
        int[] arr = { 1, 2, 3, 4, 5 };
        Assert.Equal(Arrays.SumWithForeach(arr), Arrays.SumWithFor(arr));
        Assert.Equal(15, Arrays.SumWithForeach(arr));
    }

    [Fact]
    public void LessonF_FillWithIndexSquaresProducesSquares()
        => Assert.Equal(new[] { 0, 1, 4, 9 }, Arrays.FillWithIndexSquares(4));

    // ── Lesson G: utilities ──

    [Fact]
    public void LessonG_SortInPlaceMutatesTheInput()
    {
        int[] arr = { 3, 1, 2 };
        Arrays.SortInPlace(arr);
        Assert.Equal(new[] { 1, 2, 3 }, arr);
    }

    [Fact]
    public void LessonG_ReverseInPlaceMutatesTheInput()
    {
        int[] arr = { 1, 2, 3 };
        Arrays.ReverseInPlace(arr);
        Assert.Equal(new[] { 3, 2, 1 }, arr);
    }

    [Fact]
    public void LessonG_FindIndexOfReturnsMinusOneWhenNotFound()
    {
        Assert.Equal(1, Arrays.FindIndexOf(new[] { 10, 20, 30 }, 20));
        Assert.Equal(-1, Arrays.FindIndexOf(new[] { 10, 20, 30 }, 99));
    }
}
