namespace BankApp.account;

public class Account
{
    private readonly Ledger<Transaction> _transactions;

    public string AccountNumber { get; }
    public string Holder { get; }
    public virtual decimal OverdraftLimit => 0m;

    public Account(string accountNumber, string holder, decimal startingBalance)
    {
        if (startingBalance < 0)
        {
            throw new ArgumentException("Starting balance must be greater than 0");
        }

        AccountNumber = accountNumber;
        Holder = holder;
        _transactions = new Ledger<Transaction>();
        if (startingBalance > 0)
        {
            _transactions.Add(new Transaction(TransactionType.Credit, startingBalance, TransactionCategory.Other,
                "Opening deposit"));
        }
    }

    public decimal Balance
    {
        get
        {
            decimal total = 0;
            foreach (Transaction transaction in _transactions)
            {
                switch (transaction.Type)
                {
                    case TransactionType.Credit:
                        total += transaction.Amount;
                        break;
                    case TransactionType.Debit:
                        total -= transaction.Amount;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(transaction.Type));
                }
            }

            return total;
        }
    }

    public int TransactionCount => _transactions.Count;

    public IReadOnlyList<Transaction> Transactions => _transactions.Entries;

    protected void RecordCredit(decimal amount, DateTime timestamp, TransactionCategory category, string description)
    {
        decimal newBalance = Balance + amount;
        string desc = description + $" (New Balance: {newBalance:N2})";
        _transactions.Add(new Transaction(TransactionType.Credit, amount, category, timestamp, desc));
    }

    protected void RecordDebit(decimal amount, DateTime timestamp, TransactionCategory category, string description)
    {
        decimal newBalance = Balance - amount;
        string desc = description + $" (New Balance: {newBalance:N2})";
        _transactions.Add(new Transaction(TransactionType.Debit, amount, category, timestamp, desc));
    }

    public void Deposit(decimal amount, TransactionCategory category = TransactionCategory.Other,
        string description = "Deposit")
    {
        Deposit(amount, DateTime.UtcNow, category, description);
    }

    public void Deposit(decimal amount, DateTime timestamp, TransactionCategory category = TransactionCategory.Other,
        string description = "Deposit")
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        RecordCredit(amount, timestamp, category, description);
    }

    public void Withdraw(decimal amount, TransactionCategory category = TransactionCategory.Other,
        string description = "Withdrawal")
    {
        Withdraw(amount, DateTime.UtcNow, category, description);
    }

    public virtual void Withdraw(decimal amount, DateTime timestamp,
        TransactionCategory category = TransactionCategory.Other,
        string description = "Withdrawal")
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        if (amount > Balance)
        {
            throw new InsufficientFundsException(amount, Balance);
        }

        RecordDebit(amount, timestamp, category, description);
    }

    public virtual void ApplyInterest(decimal rate)
    {
    }

    private string BuildStatement(IEnumerable<Transaction> includedTransactions)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Account Number: {AccountNumber}");
        sb.AppendLine($"Holder: {Holder}");
        sb.AppendLine($"Balance: {Balance:N2}");
        sb.AppendLine($"Overdraft Limit: {OverdraftLimit:N2}");
        sb.AppendLine("Transactions:");
        foreach (Transaction transaction in includedTransactions)
        {
            sb.AppendLine(
                $"{transaction.Timestamp:yyyy-MM-dd HH:mm} {transaction.Type.ToString().ToUpper()} {transaction.Amount:N2} {transaction.Description}");
        }

        return sb.ToString();
    }

    public string Statement()
    {
        return BuildStatement(_transactions);
    }

    public string Statement(DateTime from, DateTime to)
    {
        List<Transaction> transactionsInRange =
            _transactions.Where(t => t.Timestamp >= from && t.Timestamp <= to).ToList();
        return BuildStatement(transactionsInRange);
    }

    public List<Transaction> FindTransactions(string search)
    {
        return _transactions.Where(t => t.Description.Contains(search, StringComparison.OrdinalIgnoreCase))
            .OrderBy(t => t.Timestamp).ToList();
    }

    public List<Transaction> FindTransactions(TransactionCategory category)
    {
        return _transactions.Where(t => t.Category == category).ToList();
    }
}
