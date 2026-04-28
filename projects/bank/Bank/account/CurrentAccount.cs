namespace BankApp.account;

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

    public override void Withdraw(TransactionRequest req, DateTime? timestamp = null)
    {
        var transactionProps = CreateDebitProps(req);
        decimal availableBalance = Balance + OverdraftLimit;
        if (transactionProps.Amount > availableBalance)
        {
            throw new InsufficientFundsException(transactionProps.Amount, availableBalance);
        }

        RecordDebit(transactionProps, timestamp ?? DateTime.UtcNow);
    }
}
