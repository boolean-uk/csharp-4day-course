using OopWarmup;

namespace OopWarmup.Tests;

public class Vector2DTests
{
    // ── EXERCISE 1: DotProduct ─────────────────────────────────────

    [Fact]
    public void DotProduct_ReturnsSumOfComponentProducts()
    {
        Vector2D a = new Vector2D(3, 4);
        Vector2D b = new Vector2D(1, 2);
        Assert.Equal(11, a.DotProduct(b)); // 3*1 + 4*2 = 11
    }

    [Fact]
    public void DotProduct_OfPerpendicularVectors_IsZero()
    {
        Vector2D east  = new Vector2D(1, 0);
        Vector2D north = new Vector2D(0, 1);
        Assert.Equal(0, east.DotProduct(north));
    }

    [Fact]
    public void DotProduct_OfSelf_EqualsMagnitudeSquared()
    {
        // A·A = |A|² — a useful geometric identity, and a quick self-check.
        Vector2D v = new Vector2D(3, 4);
        Assert.Equal(25, v.DotProduct(v), precision: 9); // |v|² = 5² = 25
    }

    // ── EXERCISE 2: ToString override ──────────────────────────────

    [Fact]
    public void ToString_FormatsAsBracketedPair()
    {
        Vector2D v = new Vector2D(3, 4);
        Assert.Equal("[3, 4]", v.ToString());
    }

    [Fact]
    public void ToString_CalledImplicitlyByInterpolation()
    {
        // $"{v}" calls v.ToString() under the hood — same method as
        // Console.WriteLine(v) would use.
        Vector2D v = new Vector2D(1, 2);
        Assert.Equal("v = [1, 2]", $"v = {v}");
    }
}
