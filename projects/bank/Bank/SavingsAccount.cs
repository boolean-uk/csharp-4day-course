namespace BankApp;

public class SavingsAccount : Account
{
    public SavingsAccount(string accountNumber, string holder, decimal startingBalance)
        : base(accountNumber, holder, startingBalance)
    {
    }

    public override void ApplyInterest(decimal rate)
    {
        if (rate <= 0)
        {
            throw new ArgumentException("Rate must be greater than 0");
        }

        if (Balance <= 0)
        {
            throw new InvalidOperationException("Balance must be greater than 0");
        }

        RecordCredit(Balance * rate, DateTime.UtcNow, TransactionCategory.Interest, $"Interest {rate:P2}");
    }
}
