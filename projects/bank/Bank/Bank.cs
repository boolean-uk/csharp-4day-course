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

    public Account OpenAccount(string holder, decimal startingBalance, decimal overdraftLimit = 0m)
    {
        string accountNumber = $"ACC-{_nextAccountNumber}";
        _nextAccountNumber++;
        Account account = new Account(accountNumber, holder, startingBalance, overdraftLimit);
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

        if (fromAccount.Balance < amount)
        {
            throw new InsufficientFundsException(amount, fromAccount.Balance,
                $"Insufficient funds in {fromAccountNumber}");
        }

        fromAccount.Withdraw(amount, TransactionCategory.Transfer, $"Transfer to {toAccountNumber}");
        toAccount.Deposit(amount, TransactionCategory.Transfer, $"Transfer from {fromAccountNumber}");
    }

    public void ApplyInterest(decimal rate)
    {
        foreach (Account account in _accounts.Where(a => a.Balance > 0))
        {
            account.Deposit(account.Balance * rate, TransactionCategory.Interest, $"Interest {rate:P2}");
        }
    }
}
