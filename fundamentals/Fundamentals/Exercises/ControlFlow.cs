namespace Fundamentals.Exercises;

// Theme: Control flow — exercises for you to implement.
// The teaching material for this theme is in Lessons/ControlFlow.cs — read that first.
public static class ControlFlow
{
    // EXERCISE 1: Sign
    // Return "negative", "zero", or "positive" for the given integer.
    // Example: Sign(-3) → "negative"; Sign(0) → "zero"; Sign(7) → "positive"
    // Hint: if / else if / else chain. See Lesson A.
    public static string Sign(int n)
    {
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

    // EXERCISE 2: IsLeapYear
    // Return true if `year` is a leap year under the Gregorian rules:
    //   - divisible by 4, EXCEPT
    //   - years divisible by 100 are NOT leap years, EXCEPT
    //   - years divisible by 400 ARE leap years
    // Example: IsLeapYear(2024) → true; IsLeapYear(1900) → false; IsLeapYear(2000) → true
    // Hint: combine conditions with && and ||, or nest ifs. Either approach works.
    public static bool IsLeapYear(int year)
    {
        if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
        {
            return true;
        }

        return false;
    }

    // EXERCISE 3: DayName
    // Return the 3-letter name of the day for values 1..7, or "?" for anything else.
    // Example: DayName(1) → "Mon"; DayName(7) → "Sun"; DayName(42) → "?"
    // Hint: switch STATEMENT with 7 cases + default. See Lesson D.
    public static string DayName(int day)
    {
        switch (day)
        {
            case 1:
                return "Mon";
            case 2:
                return "Tue";
            case 3:
                return "Wed";
            case 4:
                return "Thu";
            case 5:
                return "Fri";
            case 6:
                return "Sat";
            case 7:
                return "Sun";
            default:
                return "?";
        }
    }

    // EXERCISE 4: IsVowel
    // Return true if `c` is a vowel (a, e, i, o, u), case-insensitive.
    // Example: IsVowel('a') → true; IsVowel('E') → true; IsVowel('z') → false
    // Hint: switch STATEMENT with stacked case labels. Lowercase first via char.ToLower(c).
    public static bool IsVowel(char c)
    {
        switch (char.ToLower(c))
        {
            case 'a':
            case 'e':
            case 'i':
            case 'o':
            case 'u':
                return true;
            default:
                return false;
        }
    }

    // EXERCISE 5: CountPositives
    // Return how many elements of `nums` are strictly positive (> 0).
    // Example: CountPositives([3, -1, 0, 7, -5]) → 2
    // Hint: foreach + if. See Lesson H.
    public static int CountPositives(int[] nums)
    {
        int count = 0;
        foreach (int number in nums)
        {
            if (number > 0)
            {
                count++;
            }
        }

        return count;
    }

    // EXERCISE 6: FirstIndexOf
    // Return the INDEX of the first occurrence of `target` in `nums`, or -1 if not found.
    // Example: FirstIndexOf([10, 20, 30, 20], 20) → 1; FirstIndexOf([1, 2, 3], 9) → -1
    // Hint: classic `for` loop — you need the index here, not just the value.
    //       Return `i` as soon as you find a match.
    public static int FirstIndexOf(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == target)
            {
                return i;
            }
        }

        return -1;
    }

    // EXERCISE 7: SumUntilNegative
    // Sum the elements of `nums`, but STOP as soon as you hit a negative number.
    // The negative number itself is NOT included in the sum.
    // Example: SumUntilNegative([1, 2, 3, -1, 10]) → 6; SumUntilNegative([1, 2, 3]) → 6
    // Hint: foreach + if + break. See Lesson F for break.
    public static int SumUntilNegative(int[] nums)
    {
        int sum = 0;
        foreach (int number in nums)
        {
            if (number < 0)
            {
                break;
            }

            sum += number;
        }

        return sum;
    }

    // EXERCISE 8: ReverseArray
    // Return a NEW int[] with the elements of `nums` in reverse order.
    // Do NOT modify the input array.
    // Example: ReverseArray([1, 2, 3, 4]) → [4, 3, 2, 1]
    // Hint: allocate `new int[nums.Length]`, then use a for loop to copy
    //       nums[nums.Length - 1 - i] into result[i].
    public static int[] ReverseArray(int[] nums)
    {
        int[] result = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            result[i] = nums[nums.Length - 1 - i];
        }
        return result;
    }
}
