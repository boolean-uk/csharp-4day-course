using Fundamentals.Lessons;

namespace Fundamentals.Tests.Lessons;

// Guards for the teaching examples in Lessons/Exceptions.cs.
public class ExceptionsTests
{
    // ── Lesson A: try/catch ─────────────────────────────────────────

    [Fact]
    public void LessonA_ParseOrFallback_ReturnsParsedValueOnGoodInput()
        => Assert.Equal(42, Exceptions.ParseOrFallback("42", -1));

    [Fact]
    public void LessonA_ParseOrFallback_ReturnsFallbackOnBadInput()
        => Assert.Equal(-1, Exceptions.ParseOrFallback("abc", -1));

    // ── Lesson B: throw ─────────────────────────────────────────────

    [Fact]
    public void LessonB_Divide_ReturnsQuotient()
        => Assert.Equal(5, Exceptions.Divide(10, 2));

    [Fact]
    public void LessonB_Divide_ThrowsOnZeroDivisor()
        => Assert.Throws<ArgumentException>(() => Exceptions.Divide(10, 0));

    [Fact]
    public void LessonB_Divide_ThrownExceptionNamesTheParameter()
    {
        ArgumentException ex = Assert.Throws<ArgumentException>(() => Exceptions.Divide(10, 0));
        Assert.Equal("b", ex.ParamName);
    }

    // ── Lesson C: catching what you threw ───────────────────────────

    [Fact]
    public void LessonC_SafeDivideAsMessage_ReturnsQuotientText()
        => Assert.Equal("5", Exceptions.SafeDivideAsMessage(10, 2));

    [Fact]
    public void LessonC_SafeDivideAsMessage_ReturnsErrorTextOnZeroDivisor()
        => Assert.StartsWith("error:", Exceptions.SafeDivideAsMessage(10, 0));

    // ── Lesson D: catch the specific type ───────────────────────────

    [Fact]
    public void LessonD_SwallowEverything_AntiPatternReturnsZeroOnBadInput()
        => Assert.Equal(0, Exceptions.SwallowEverything_AntiPattern("abc"));

    // This is the whole point of Lesson D: a bare `catch` silently
    // turns a programmer bug (passing null) into a wrong answer. The
    // method returns 0 as if the input had simply been unparseable.
    [Fact]
    public void LessonD_SwallowEverything_HidesNullBugSilently()
        => Assert.Equal(0, Exceptions.SwallowEverything_AntiPattern(null!));

    // Contrast: the tight catch in Lesson A's ParseOrFallback only
    // swallows FormatException. A null input bubbles up as
    // ArgumentNullException — exposing the bug instead of hiding it.
    [Fact]
    public void LessonD_Contrast_ParseOrFallbackLetsNullBugBubbleUp()
        => Assert.Throws<ArgumentNullException>(() => Exceptions.ParseOrFallback(null!, -1));

    // ── Lesson E: prefer non-throwing alternatives ──────────────────

    [Fact]
    public void LessonE_TryDivide_ReturnsTrueAndQuotientOnSuccess()
    {
        bool ok = Exceptions.TryDivide(10, 2, out int result);
        Assert.True(ok);
        Assert.Equal(5, result);
    }

    [Fact]
    public void LessonE_TryDivide_ReturnsFalseAndZeroOnZeroDivisor()
    {
        bool ok = Exceptions.TryDivide(10, 0, out int result);
        Assert.False(ok);
        Assert.Equal(0, result);
    }

    [Fact]
    public void LessonE_ParsePreferred_ReturnsValueOnGoodInput()
        => Assert.Equal(42, Exceptions.ParsePreferred("42", -1));

    [Fact]
    public void LessonE_ParsePreferred_ReturnsFallbackOnBadInput()
        => Assert.Equal(-1, Exceptions.ParsePreferred("abc", -1));
}
