namespace BankApp.transaction;

public struct Transaction
{
    public TransactionType Type { get; }
    public decimal Amount { get; }
    public TransactionCategory Category { get; }
    public DateTime Timestamp { get; }
    public string Description { get; }

    public Transaction(TransactionType type, decimal amount, TransactionCategory category, string description)
        : this(type, amount, category, DateTime.UtcNow, description)
    {
    }

    public Transaction(
        TransactionType type,
        decimal amount,
        TransactionCategory category,
        DateTime timestamp,
        string description)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        Type = type;
        Amount = amount;
        Category = category;
        Timestamp = timestamp;
        Description = description;
    }
}