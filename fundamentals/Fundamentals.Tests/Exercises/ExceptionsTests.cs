using Fundamentals.Exercises;

namespace Fundamentals.Tests.Exercises;

// Tests for the exercises in Exercises/Exceptions.cs.
// These will fail until the student implements each method.
public class ExceptionsTests
{
    // EXERCISE 1: RequirePositive

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(1_000_000)]
    public void RequirePositive_ReturnsInputWhenPositive(int n)
        => Assert.Equal(n, Exceptions.RequirePositive(n));

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void RequirePositive_ThrowsWhenZeroOrNegative(int n)
        => Assert.Throws<ArgumentException>(() => Exceptions.RequirePositive(n));

    [Fact]
    public void RequirePositive_ThrownExceptionNamesTheParameter()
    {
        ArgumentException ex = Assert.Throws<ArgumentException>(() => Exceptions.RequirePositive(-1));
        Assert.Equal("n", ex.ParamName);
    }

    // EXERCISE 2: Withdraw

    [Theory]
    [InlineData(100, 30, 70)]
    [InlineData(100, 100, 0)]       // exactly-equal is allowed
    [InlineData(50, 0, 50)]         // withdrawing zero is fine
    public void Withdraw_ReturnsNewBalanceOnSuccess(int balance, int amount, int expected)
        => Assert.Equal(expected, Exceptions.Withdraw(balance, amount));

    [Theory]
    [InlineData(50, 100)]
    [InlineData(0, 1)]
    public void Withdraw_ThrowsWhenAmountExceedsBalance(int balance, int amount)
        => Assert.Throws<InvalidOperationException>(() => Exceptions.Withdraw(balance, amount));

    // EXERCISE 3: SafeWithdraw

    [Theory]
    [InlineData(100, 30, 70)]
    [InlineData(100, 100, 0)]
    public void SafeWithdraw_ReturnsNewBalanceOnSuccess(int balance, int amount, int expected)
        => Assert.Equal(expected, Exceptions.SafeWithdraw(balance, amount));

    [Theory]
    [InlineData(50, 100, 50)]       // refused — balance unchanged
    [InlineData(0, 1, 0)]
    public void SafeWithdraw_ReturnsOriginalBalanceOnRefusal(int balance, int amount, int expected)
        => Assert.Equal(expected, Exceptions.SafeWithdraw(balance, amount));
}
