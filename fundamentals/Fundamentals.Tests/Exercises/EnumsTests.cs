using Fundamentals.Exercises;

namespace Fundamentals.Tests.Exercises;

// Tests for the exercises in Exercises/Enums.cs.
// These will fail until the student implements each method.
public class EnumsTests
{
    // EXERCISE 1: Prompt

    [Theory]
    [InlineData(VendingMachineState.Idle, "Insert a coin")]
    [InlineData(VendingMachineState.CoinInserted, "Select a product")]
    [InlineData(VendingMachineState.Dispensing, "Please wait...")]
    [InlineData(VendingMachineState.OutOfStock, "Sold out")]
    public void Prompt_ReturnsExpectedMessage(VendingMachineState state, string expected)
        => Assert.Equal(expected, Enums.Prompt(state));

    // EXERCISE 2: CanAcceptCoin

    [Theory]
    [InlineData(VendingMachineState.Idle, true)]
    [InlineData(VendingMachineState.CoinInserted, false)]
    [InlineData(VendingMachineState.Dispensing, false)]
    [InlineData(VendingMachineState.OutOfStock, false)]
    public void CanAcceptCoin_TrueOnlyWhenIdle(VendingMachineState state, bool expected)
        => Assert.Equal(expected, Enums.CanAcceptCoin(state));

    // EXERCISE 3: ParseState

    [Theory]
    [InlineData("Idle", VendingMachineState.Idle)]
    [InlineData("idle", VendingMachineState.Idle)]             // case-insensitive
    [InlineData("DISPENSING", VendingMachineState.Dispensing)] // case-insensitive
    [InlineData("CoinInserted", VendingMachineState.CoinInserted)]
    public void ParseState_ReturnsTrueAndValueOnMatch(string text, VendingMachineState expected)
    {
        bool ok = Enums.ParseState(text, out VendingMachineState state);
        Assert.True(ok);
        Assert.Equal(expected, state);
    }

    [Theory]
    [InlineData("dancing")]
    [InlineData("")]
    [InlineData("Idlee")]
    public void ParseState_ReturnsFalseOnBadInput(string text)
    {
        bool ok = Enums.ParseState(text, out _);
        Assert.False(ok);
    }
}
