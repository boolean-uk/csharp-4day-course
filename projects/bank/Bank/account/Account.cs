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
            var transactionProps = new TransactionProps
            {
                Type = TransactionType.Credit,
                Amount = startingBalance,
                Category = TransactionCategory.Other,
                Description = "Opening deposit"
            };
            _transactions.Add(new Transaction(transactionProps));
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

    protected static TransactionProps CreateCreditProps(TransactionRequest req)
    {
        if (req.Amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        return new TransactionProps
        {
            Type = TransactionType.Credit,
            Amount = req.Amount,
            Category = req.Category ?? TransactionCategory.Other,
            Description = req.Description ?? "Deposit",
        };
    }

    protected void RecordCredit(TransactionProps props, DateTime? timestamp = null)
    {
        decimal newBalance = Balance + props.Amount;
        string desc = props.Description + $" (New Balance: {newBalance:N2})";
        var transactionProps = props with
        {
            Description = desc,
        };
        _transactions.Add(timestamp is not null
            ? new Transaction(transactionProps, timestamp.Value)
            : new Transaction(transactionProps));
    }

    protected static TransactionProps CreateDebitProps(TransactionRequest req)
    {
        if (req.Amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        return new TransactionProps
        {
            Type = TransactionType.Debit,
            Amount = req.Amount,
            Category = req.Category ?? TransactionCategory.Other,
            Description = req.Description ?? "Withdrawal",
        };
    }

    protected void RecordDebit(TransactionProps props, DateTime? timestamp = null)
    {
        decimal newBalance = Balance - props.Amount;
        string desc = props.Description + $" (New Balance: {newBalance:N2})";
        var transactionProps = props with
        {
            Description = desc,
        };
        _transactions.Add(timestamp is not null
            ? new Transaction(transactionProps, timestamp.Value)
            : new Transaction(transactionProps));
    }

    public void Deposit(TransactionRequest req, DateTime? timestamp = null)
    {
        var transactionProps = CreateCreditProps(req);
        RecordCredit(transactionProps, timestamp ?? DateTime.UtcNow);
    }

    public virtual void Withdraw(TransactionRequest req, DateTime? timestamp = null)
    {
        var transactionProps = CreateDebitProps(req);
        if (transactionProps.Amount > Balance)
        {
            throw new InsufficientFundsException(transactionProps.Amount, Balance);
        }

        RecordDebit(transactionProps, timestamp ?? DateTime.UtcNow);
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
