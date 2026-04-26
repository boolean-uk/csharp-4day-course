Bank bank = new Bank("Acme Savings");
Account a = bank.OpenSavingsAccount("Ada Lovelace", 200m);
Account b = bank.OpenSavingsAccount("Alan Turing", 200m);
Account c = bank.OpenCurrentAccount("Bob Smith", 0m, 100m);
c.Withdraw(50m);
a.Deposit(50m);
a.Withdraw(20m);
b.Deposit(20m);
bank.Transfer(a.AccountNumber, b.AccountNumber, 50m);
Console.WriteLine(a.Statement());
Console.WriteLine(b.Statement());
Console.WriteLine(c.Statement());
Console.WriteLine(bank.TotalAssets);

bank.ApplyInterest(0.05m);

Console.WriteLine(a.Statement());
Console.WriteLine(b.Statement());
Console.WriteLine(c.Statement());