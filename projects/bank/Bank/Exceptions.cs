namespace BankApp;

public class InsufficientFundsException : Exception
{
    public decimal RequestedAmount { get; }
    public decimal AvailableBalance { get; }

    public InsufficientFundsException(decimal requestedAmount, decimal availableBalance,
        string message = "Insufficient funds") : base(message)
    {
        RequestedAmount = requestedAmount;
        AvailableBalance = availableBalance;
    }
}