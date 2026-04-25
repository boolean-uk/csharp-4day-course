namespace BankApp;

public class CurrentAccount : Account
{
    public override decimal OverdraftLimit { get; }

    public CurrentAccount(string accountNumber, string holder, decimal startingBalance, decimal overdraftLimit)
        : base(accountNumber, holder, startingBalance)
    {
        if (overdraftLimit < 0)
        {
            throw new ArgumentException("Overdraft limit must be greater than or equal to 0");
        }

        OverdraftLimit = overdraftLimit;
    }

    public override void Withdraw(decimal amount, DateTime timestamp,
        TransactionCategory category = TransactionCategory.Other,
        string description = "Withdrawal")
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        decimal availableBalance = Balance + OverdraftLimit;
        if (amount > availableBalance)
        {
            throw new InsufficientFundsException(amount, availableBalance);
        }

        RecordDebit(amount, timestamp, category, description);
    }
}
