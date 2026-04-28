namespace BankApp.account;

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

        var transactionProps = CreateCreditProps(new TransactionRequest
        {
            Amount = Balance * rate,
            Category = TransactionCategory.Interest,
            Description = $"Interest {rate:P2}"
        });
        RecordCredit(transactionProps);
    }
}
