namespace BankApp.transaction;

public struct Transaction
{
    public TransactionType Type { get; }
    public decimal Amount { get; }
    public TransactionCategory Category { get; }
    public DateTime Timestamp { get; }
    public string Description { get; }

    public Transaction(TransactionProps props) : this(props, DateTime.UtcNow)
    {
    }

    public Transaction(TransactionProps props, DateTime timestamp)
    {
        if (props.Amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        Type = props.Type;
        Amount = props.Amount;
        Category = props.Category;
        Timestamp = timestamp;
        Description = props.Description;
    }
}