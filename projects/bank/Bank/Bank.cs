namespace BankApp;

public class Bank
{
    private readonly List<Account> _accounts;
    private int _nextAccountNumber;

    public string Name { get; }

    public Bank(string name)
    {
        Name = name;
        _accounts = new List<Account>();
        _nextAccountNumber = 1000;
    }

    public int AccountCount => _accounts.Count;

    public decimal TotalAssets
    {
        get { return _accounts.Sum(a => a.Balance); }
    }

    public IReadOnlyList<Account> Accounts => _accounts.AsReadOnly();

    public SavingsAccount OpenSavingsAccount(string holder, decimal startingBalance)
    {
        string accountNumber = $"ACC-{_nextAccountNumber}";
        _nextAccountNumber++;
        SavingsAccount account = new SavingsAccount(accountNumber, holder, startingBalance);
        _accounts.Add(account);
        return account;
    }

    public CurrentAccount OpenCurrentAccount(string holder, decimal startingBalance, decimal overdraftLimit)
    {
        string accountNumber = $"ACC-{_nextAccountNumber}";
        _nextAccountNumber++;
        CurrentAccount account = new CurrentAccount(accountNumber, holder, startingBalance, overdraftLimit);
        _accounts.Add(account);
        return account;
    }

    public Account? FindAccount(string accountNumber)
    {
        return _accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
    }

    public bool CloseAccount(string accountNumber)
    {
        Account? account = FindAccount(accountNumber);
        if (account != null)
        {
            _accounts.Remove(account);
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

        fromAccount.Withdraw(new TransactionRequest
        {
            Amount = amount, Category = TransactionCategory.Transfer, Description = $"Transfer to {toAccountNumber}"
        });
        toAccount.Deposit(new TransactionRequest
        {
            Amount = amount, Category = TransactionCategory.Transfer, Description = $"Transfer from {fromAccountNumber}"
        });
    }

    public void ApplyInterest(decimal rate)
    {
        foreach (Account account in _accounts)
        {
            account.ApplyInterest(rate);
        }
    }
}
