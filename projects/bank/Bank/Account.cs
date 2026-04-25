using System.Text;

namespace BankApp;

public class Account
{
    private readonly List<Transaction> _transactions;

    public string AccountNumber { get; }
    public string Holder { get; }
    public decimal OverdraftLimit { get; }

    public Account(string accountNumber, string holder, decimal startingBalance, decimal overdraftLimit = 0m)
    {
        if (startingBalance < 0)
        {
            throw new ArgumentException("Starting balance must be greater than 0");
        }

        AccountNumber = accountNumber;
        Holder = holder;
        _transactions = new List<Transaction>();
        OverdraftLimit = overdraftLimit;
        if (startingBalance > 0)
        {
            _transactions.Add(new Transaction(TransactionType.Credit, startingBalance, "Opening deposit"));
        }
    }

    public decimal Balance
    {
        get
        {
            decimal total = 0;
            foreach (Transaction transaction in _transactions)
            {
                if (transaction.Type == TransactionType.Credit)
                {
                    total += transaction.Amount;
                }
                else if (transaction.Type == TransactionType.Debit)
                {
                    total -= transaction.Amount;
                }
            }

            return total;
        }
    }

    public int TransactionCount => _transactions.Count;

    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();

    public void Deposit(decimal amount, string description = "Deposit")
    {
        Deposit(amount, DateTime.UtcNow, description);
    }

    public void Deposit(decimal amount, DateTime timestamp, string description = "Deposit")
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        decimal newBalance = Balance + amount;
        string desc = description + $" (New Balance: {newBalance:N2})";
        _transactions.Add(new Transaction(TransactionType.Credit, amount, timestamp, desc));
    }

    public void Withdraw(decimal amount, string description = "Withdrawal")
    {
        Withdraw(amount, DateTime.UtcNow, description);
    }

    public void Withdraw(decimal amount, DateTime timestamp, string description = "Withdrawal")
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        if (amount > Balance + OverdraftLimit)
        {
            throw new InvalidOperationException("Amount must be less than or equal to Balance");
        }

        decimal newBalance = Balance - amount;
        string desc = description + $" (New Balance: {newBalance:N2})";
        _transactions.Add(new Transaction(TransactionType.Debit, amount, timestamp, desc));
    }

    private string BuildStatement(List<Transaction> includedTransactions)
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
}
