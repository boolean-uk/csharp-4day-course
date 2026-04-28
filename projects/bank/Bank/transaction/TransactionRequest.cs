namespace BankApp.transaction;

public record TransactionRequest
{
    public decimal Amount { get; init; }
    public TransactionCategory? Category { get; init; }
    public string? Description { get; init; }
}