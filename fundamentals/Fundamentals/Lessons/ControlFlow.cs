namespace Fundamentals.Lessons;

// Theme: Control flow — if / else if, switch, ternary, while, for, foreach.
// Assumes you already know these constructs from JavaScript/Python — we focus
// on what's different or surprising in C#.
//
// This file contains the TEACHING EXAMPLES only. Your work lives in
// Exercises/ControlFlow.cs.
public static class ControlFlow
{
    // ─────────────────────────────────────────────────────────────
    // LESSON A: `if` syntax differences
    // ─────────────────────────────────────────────────────────────
    // Three things to notice compared with Python/JS:
    //   1) The condition MUST be wrapped in parentheses: `if (x > 0) ...`
    //   2) The body uses { braces }, not Python-style indentation.
    //   3) The condition must be a bool. Unlike JavaScript, C# does NOT do
    //      "truthy" coercion: `if (1)` and `if ("hi")` are COMPILE ERRORS.
    //      Always compare explicitly — `if (count > 0)`, `if (name != null)`,
    //      `if (nums.Length > 0)`.

    public static string DescribeSign(int n)
    {
        // e.g. DescribeSign(-3) == "negative"; DescribeSign(0) == "zero"; DescribeSign(7) == "positive"
        if (n > 0)
        {
            return "positive";
        }
        else if (n < 0)
        {
            return "negative";
        }
        else
        {
            return "zero";
        }
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON B: The "no braces" gotcha
    // ─────────────────────────────────────────────────────────────
    // C# lets you OMIT braces for a single-statement branch:
    //     if (cond) doOneThing();
    // ...which looks harmless until someone adds a second line. Without
    // braces, only the FIRST statement is part of the if. The indentation
    // means nothing to the compiler.

    // GOTCHA: the `b += 10` line LOOKS nested under the if, but it isn't.
    // It runs every time, regardless of `a`.
    public static int NoBracesGotcha(int a, int b)
    {
        // e.g. NoBracesGotcha(0, 0) == 10  ← b += 10 ran even though a is not > 0
        //      NoBracesGotcha(1, 0) == 11  ← both lines ran
        if (a > 0)
            b += 1;
            b += 10; // ← ALWAYS runs, despite the misleading indentation
        return b;
    }

    // FIX: always use braces, even for one-line branches. Clear, unambiguous,
    // and safe against future edits.
    public static int NoBracesFixed(int a, int b)
    {
        // e.g. NoBracesFixed(0, 0) == 0
        //      NoBracesFixed(1, 0) == 11
        if (a > 0)
        {
            b += 1;
            b += 10;
        }
        return b;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON C: `else if` chains — a classifier
    // ─────────────────────────────────────────────────────────────
    // Evaluated top-to-bottom; the first matching branch wins and the rest
    // are skipped. Same shape as JS/Python.
    public static string Grade(int mark)
    {
        // e.g. Grade(85) == "A"; Grade(65) == "B"; Grade(55) == "C"; Grade(40) == "F"
        if (mark >= 70)
        {
            return "A";
        }
        else if (mark >= 60)
        {
            return "B";
        }
        else if (mark >= 50)
        {
            return "C";
        }
        else
        {
            return "F";
        }
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON D: `switch` statement — and the break flip
    // ─────────────────────────────────────────────────────────────
    // The classic C-style switch exists in C#, with two things to know:
    //
    //   1) Each case body MUST exit explicitly — with `break`, `return`,
    //      `throw`, or `goto case X`. Unlike JavaScript, C# does NOT allow
    //      implicit fall-through from one case body to the next — it's a
    //      COMPILE ERROR. This flips JS's #1 switch bug on its head.
    //
    //   2) You CAN stack multiple case labels on the same body. That's how
    //      you express "any of these values share this behaviour".

    public static string DayKind(int day)
    {
        // e.g. DayKind(1) == "weekday"; DayKind(6) == "weekend"; DayKind(99) == "unknown"
        switch (day)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                return "weekday";
            case 6:
            case 7:
                return "weekend";
            default:
                return "unknown";
        }
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON E: Ternary operator (quick refresher)
    // ─────────────────────────────────────────────────────────────
    // `condition ? whenTrue : whenFalse` — identical to JavaScript, same role
    // as Python's `whenTrue if condition else whenFalse`. Use it for one-line
    // either/or choices; reach for `if` when the branches have real logic.
    public static string Parity(int n)
    {
        // e.g. Parity(4) == "even"; Parity(7) == "odd"
        return n % 2 == 0 ? "even" : "odd";
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON F: `while` loop + `break`
    // ─────────────────────────────────────────────────────────────
    // Standard `while (cond) { ... }` loop — runs while the condition is true.
    // `break` exits the nearest enclosing loop immediately.
    //
    // Two flavours worth knowing:

    // Standard shape — condition checked at the top of each iteration.
    public static int CountHalvings(int n)
    {
        // How many times can we halve `n` (integer division) before it hits 0?
        // e.g. CountHalvings(8) → 8/2=4, /2=2, /2=1, /2=0  → 4 halvings
        int count = 0;
        while (n > 0)
        {
            n = n / 2;
            count++;
        }
        return count;
    }

    // `while (true) { ... if (done) break; }` — common idiom when the exit
    // condition is awkward to express at the top of the loop.
    public static int FirstMultipleOfThree(int start)
    {
        // e.g. FirstMultipleOfThree(10) == 12; FirstMultipleOfThree(9) == 9
        int n = start;
        while (true)
        {
            if (n % 3 == 0)
            {
                break;
            }
            n++;
        }
        return n;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON G: Classic `for` loop
    // ─────────────────────────────────────────────────────────────
    // Same three-part header as JavaScript:
    //     for (initializer; condition; step) { body }
    // The variable declared in the header is scoped to the loop.
    //
    // To get an array's size use the `.Length` PROPERTY — no parentheses,
    // not `len(arr)` (Python) or `arr.length` with a lowercase l (JS).
    public static int SumArray(int[] nums)
    {
        // e.g. SumArray([1, 2, 3, 4]) == 10
        int total = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            total += nums[i];
        }
        return total;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON H: `foreach` over an array
    // ─────────────────────────────────────────────────────────────
    // `foreach` iterates VALUES (not indexes). Closest analogues:
    //     JS:     for (const x of xs) { ... }
    //     Python: for x in xs:
    //
    // Syntax: you must declare the element type up front:
    //     foreach (int n in nums) { ... }
    // Reach for `foreach` when you only care about the values. Reach for
    // `for` when you need the index (or to iterate in a non-trivial order).
    public static int MaxValue(int[] nums)
    {
        // e.g. MaxValue([3, 7, 1, 9, 4]) == 9
        // (Assumes at least one element — empty arrays have no max.)
        int best = nums[0];
        foreach (int n in nums)
        {
            if (n > best)
            {
                best = n;
            }
        }
        return best;
    }
}
