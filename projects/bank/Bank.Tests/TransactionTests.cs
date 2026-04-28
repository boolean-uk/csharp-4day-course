namespace BankApp.Tests;

public class TransactionTests
{
    [Fact]
    public void Constructor_AssignsAllProperties()
    {
        Transaction t = new Transaction(new TransactionProps
        {
            Type = TransactionType.Credit, Amount = 100m, Category = TransactionCategory.Other,
            Description = "Opening deposit"
        });
        Assert.Equal(TransactionType.Credit, t.Type);
        Assert.Equal(100m, t.Amount);
        Assert.Equal("Opening deposit", t.Description);
    }

    [Fact]
    public void Constructor_StampsTimestampCloseToNow()
    {
        DateTime before = DateTime.UtcNow;
        Transaction t = new Transaction(new TransactionProps
            { Type = TransactionType.Credit, Amount = 1m, Category = TransactionCategory.Other, Description = "x" });
        DateTime after = DateTime.UtcNow;
        Assert.InRange(t.Timestamp, before, after);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100.50)]
    public void Constructor_ThrowsOnNonPositiveAmount(decimal badAmount)
    {
        Assert.Throws<ArgumentException>(() =>
            new Transaction(new TransactionProps
            {
                Type = TransactionType.Credit, Amount = badAmount, Category = TransactionCategory.Other,
                Description = "x"
            }));
    }

    [Fact]
    public void Transaction_IsAValueType()
    {
        // Sanity check: Transaction is a struct (value type).
        // Copying it and mutating the copy cannot affect the original,
        // and properties have no setters so the compiler also prevents
        // mutation outright.
        Assert.True(typeof(Transaction).IsValueType);
    }
}
