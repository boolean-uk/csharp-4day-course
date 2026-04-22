namespace Fundamentals.Exercises;

// Theme: Exceptions — exercises for you to implement.
// The teaching material for this theme is in Lessons/Exceptions.cs — read that first.

public static class Exceptions
{
    // EXERCISE 1: RequirePositive
    // Return `n` unchanged when it's positive (> 0).
    // When `n` is zero or negative, throw `ArgumentException` with the
    // message "n must be positive" and the parameter name "n".
    // Example: RequirePositive(5)  → 5
    //          RequirePositive(0)  → throws ArgumentException
    //          RequirePositive(-3) → throws ArgumentException
    // Hint: Lesson B in Exceptions.cs shows the throw pattern.
    //       Use `nameof(n)` for the parameter name.
    public static int RequirePositive(int n)
    {
        throw new NotImplementedException("TODO: throw ArgumentException when n <= 0, otherwise return n");
    }

    // EXERCISE 2: Withdraw
    // Return the new balance after withdrawing `amount` from `balance`.
    // When `amount` is greater than `balance`, throw `InvalidOperationException`
    // with the message "insufficient funds".
    // Example: Withdraw(100, 30)  → 70
    //          Withdraw(100, 100) → 0
    //          Withdraw(50, 100)  → throws InvalidOperationException
    // Hint: `InvalidOperationException` is the built-in type for "the object
    //       isn't in a state where this operation makes sense".
    public static int Withdraw(int balance, int amount)
    {
        throw new NotImplementedException("TODO: throw InvalidOperationException when amount > balance, else return balance - amount");
    }

    // EXERCISE 3: SafeWithdraw
    // Call Withdraw(balance, amount). If it succeeds, return the new balance.
    // If it throws InvalidOperationException, return the ORIGINAL balance
    // unchanged (the withdrawal was refused, so the caller's balance shouldn't change).
    // Example: SafeWithdraw(100, 30)  → 70  (Withdraw succeeded)
    //          SafeWithdraw(50, 100)  → 50  (Withdraw threw, we return the unchanged balance)
    // Hint: wrap the call to Withdraw in try / catch (InvalidOperationException).
    //       Do NOT catch every exception — only the specific type you expect.
    public static int SafeWithdraw(int balance, int amount)
    {
        throw new NotImplementedException("TODO: try Withdraw; on InvalidOperationException return balance unchanged");
    }
}
