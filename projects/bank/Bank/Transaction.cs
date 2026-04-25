namespace BankApp;

public struct Transaction
{
    public TransactionType Type { get; }
    public decimal Amount { get; }
    public DateTime Timestamp { get; }
    public string Description { get; }

    public Transaction(TransactionType type, decimal amount, string description)
        : this(type, amount, DateTime.UtcNow, description)
    {
    }

    public Transaction(TransactionType type, decimal amount, DateTime timestamp, string description)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        Type = type;
        Amount = amount;
        Timestamp = timestamp;
        Description = description;
    }
}
