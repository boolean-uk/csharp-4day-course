using System.Text;

namespace BankApp;

// A bank account belonging to one holder. Keeps its own transaction history
// and computes its balance from that history (nothing else stores a balance).
//
// Key rules:
//   • The opening balance passed to the constructor is recorded as the FIRST
//     transaction (a Credit with description "Opening deposit") — UNLESS
//     startingBalance is exactly 0, in which case the history starts empty.
//   • Balance is derived: sum of Credits minus sum of Debits.
//   • Deposit and Withdraw both require a positive amount (ArgumentException
//     otherwise). Withdraw throws InvalidOperationException if the amount
//     would take the balance below zero (no overdrafts in the core spec).
//
// The `transactions` list is private so outside code can only change the
// account through Deposit / Withdraw — the balance invariant stays intact.
public class Account
{
    private List<Transaction> transactions;

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
        transactions = new List<Transaction>();
        OverdraftLimit = overdraftLimit;
        if (startingBalance > 0)
        {
            transactions.Add(new Transaction(TransactionType.Credit, startingBalance, "Opening deposit"));
        }
    }

    // Computed on every read — there's no stored balance field.
    public decimal Balance
    {
        get
        {
            decimal total = 0;
            foreach (Transaction transaction in transactions)
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

    public int TransactionCount
    {
        get { return transactions.Count; }
    }

    // Expose a read-only view — callers can enumerate but not Add/Remove.
    public IReadOnlyList<Transaction> Transactions
    {
        get { return transactions.AsReadOnly(); }
    }

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
        transactions.Add(new Transaction(TransactionType.Credit, amount, timestamp, desc));
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
        transactions.Add(new Transaction(TransactionType.Debit, amount, timestamp, desc));
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

    // Returns a printable multi-line bank statement. Format is deliberately
    // our own choice here — the tests only check that the required fields
    // appear in the output, so you're free to make it pretty.
    public string Statement()
    {
        return BuildStatement(transactions);
    }

    public string Statement(DateTime from, DateTime to)
    {
        List<Transaction> transactionsInRange =
            transactions.Where(t => t.Timestamp >= from && t.Timestamp <= to).ToList();
        return BuildStatement(transactionsInRange);
    }

    // Case-insensitive substring match on Description.
    // Results are sorted oldest-first by Timestamp.
    public List<Transaction> FindTransactions(string search)
    {
        return transactions.Where(t => t.Description.Contains(search, StringComparison.OrdinalIgnoreCase))
            .OrderBy(t => t.Timestamp).ToList();
    }
}
