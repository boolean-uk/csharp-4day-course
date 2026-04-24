namespace BankApp;

// The top-level simulation: one Bank holds many Accounts, generates account
// numbers, and exposes summary info across the whole portfolio.
//
// Account numbers are auto-generated in the form "ACC-1000", "ACC-1001", ...
// The next-number counter lives as an instance field on the Bank (one counter
// per Bank instance). Simple, private, no `static` required.
public class Bank
{
    private List<Account> accounts;
    private int nextAccountNumber;

    public string Name { get; }

    public Bank(string name)
    {
        Name = name;
        accounts = new List<Account>();
        nextAccountNumber = 1000;
    }

    public int AccountCount
    {
        get { return accounts.Count; }
    }

    // Sum of every account's balance at this moment. Computed, not stored.
    public decimal TotalAssets
    {
        get { return accounts.Sum(a => a.Balance); }
    }

    public IReadOnlyList<Account> Accounts
    {
        get { return accounts.AsReadOnly(); }
    }

    public Account OpenAccount(string holder, decimal startingBalance)
    {
        string accountNumber = $"ACC-{nextAccountNumber}";
        nextAccountNumber++;
        Account account = new Account(accountNumber, holder, startingBalance);
        accounts.Add(account);
        return account;
    }

    // Returns null when no account matches.
    public Account? FindAccount(string accountNumber)
    {
        return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
    }

    // Returns true if an account was found and removed, false otherwise.
    public bool CloseAccount(string accountNumber)
    {
        Account? account = FindAccount(accountNumber);
        if (account != null)
        {
            accounts.Remove(account);
            return true;
        }

        return false;
    }

    public void Transfer(string fromAccountNumber, string toAccountNumber, decimal amount)
    {
        Account? fromAccount = FindAccount(fromAccountNumber);
        if (fromAccount == null)
        {
            throw new InvalidOperationException("From account not found");
        }

        Account? toAccount = FindAccount(toAccountNumber);
        if (toAccount == null)
        {
            throw new InvalidOperationException("To account not found");
        }

        if (fromAccount.Balance < amount)
        {
            throw new InvalidOperationException($"Insufficient funds in {fromAccountNumber}");
        }

        fromAccount.Withdraw(amount, $"Transfer to {toAccountNumber}");
        toAccount.Deposit(amount, $"Transfer from {fromAccountNumber}");
    }
}
