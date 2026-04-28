namespace BankApp.transaction;

public record TransactionProps
{
    public required TransactionType Type { get; init; }
    public required decimal Amount { get; init; }
    public required TransactionCategory Category { get; init; }
    public required string Description { get; init; }
}