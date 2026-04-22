namespace Fundamentals.Lessons;

// Theme: Arrays — C#-specific behaviour that will surprise folks coming from
// JavaScript or Python. The three big differences are:
//   1) fixed size (you can't push/pop)
//   2) strongly typed (one type per array)
//   3) out-of-bounds access throws — no silent `undefined`
//
// This file contains the TEACHING EXAMPLES only. Exercises live in
// Exercises/Arrays.cs.
public static class Arrays
{
    // ─────────────────────────────────────────────────────────────
    // LESSON A: Declaring arrays — size is fixed at creation
    // ─────────────────────────────────────────────────────────────
    // In JS/Python you typically start with an empty array and push to it.
    // In C# arrays, you pick EITHER a size OR the initial contents when
    // you create the array. The size is fixed from that moment on.
    // There are four common forms — all of these appear in real C# code.

    // 1) new int[N] — give just the size. Contents are the type's default value.
    public static int[] EmptyWithSize()
    {
        // e.g. returns { 0, 0, 0, 0, 0 } — 5 ints, all zero (ints default to 0)
        return new int[5];
    }

    // 2) new int[] { ... } — longhand initialiser with contents.
    public static int[] LonghandInitialiser()
    {
        // e.g. returns { 10, 20, 30 } — length 3, inferred from the list
        return new int[] { 10, 20, 30 };
    }

    // 3) { ... } — shorthand, only valid right at the variable declaration.
    public static int[] ShorthandInitialiser()
    {
        int[] scores = { 10, 20, 30 };
        return scores;
    }

    // 4) [ ... ] — collection expression (C# 12+). Currently the shortest form.
    public static int[] CollectionExpression()
    {
        return [10, 20, 30];
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON B: Arrays are strongly typed — one type per array
    // ─────────────────────────────────────────────────────────────
    // An int[] holds ints ONLY. A string[] holds strings ONLY.
    // Unlike JavaScript where [1, "hi", true] is perfectly legal,
    // or Python where lists are heterogeneous by default.

    public static int[] IntsOnly()
    {
        // e.g. { 1, 2, 3 } — compile error if you tried to put a string in here
        return new int[] { 1, 2, 3 };
    }

    public static string[] StringsOnly()
    {
        // e.g. { "a", "b", "c" }
        return new string[] { "a", "b", "c" };
    }

    // If you tried the following, it wouldn't compile:
    //   var mixed = new[] { 1, "hi", true };   // error: no common type
    // C# wants one agreed type across every slot.

    // ─────────────────────────────────────────────────────────────
    // LESSON C: Default values — what's inside `new int[5]`?
    // ─────────────────────────────────────────────────────────────
    // When you create an array by size only, every slot is pre-filled with
    // the DEFAULT VALUE for that type:
    //   int       → 0
    //   double    → 0.0
    //   bool      → false
    //   string    → null   (reference types default to null)
    //
    // Worth internalising: an array of strings isn't empty strings, it's
    // NULLs. A NullReferenceException is waiting for you if you forget.

    public static int[] DefaultInts()
    {
        // e.g. { 0, 0, 0, 0, 0 }
        return new int[5];
    }

    public static double[] DefaultDoubles()
    {
        // e.g. { 0, 0, 0 }  (really 0.0 but displayed as 0)
        return new double[3];
    }

    public static bool[] DefaultBools()
    {
        // e.g. { false, false, false }
        return new bool[3];
    }

    public static string?[] DefaultStrings()
    {
        // e.g. { null, null, null }
        // Note the `?` after string — it marks the element type as nullable,
        // matching the fact that uninitialised slots really are null.
        return new string?[3];
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON D: Indexing, .Length, and mutating slots
    // ─────────────────────────────────────────────────────────────
    // Arrays are zero-indexed (same as JS/Python).
    // .Length is a PROPERTY (no parentheses), same as JS's .length.
    // Slots are mutable — you can assign new values to existing indices.

    public static int FirstElement(int[] arr)
    {
        // e.g. arr = { 10, 20, 30 }; arr[0] == 10
        return arr[0];
    }

    public static int LastElement(int[] arr)
    {
        // e.g. arr = { 10, 20, 30 }; arr[arr.Length - 1] == 30
        return arr[arr.Length - 1];
    }

    public static int HowMany(int[] arr)
    {
        // e.g. arr = { 10, 20, 30 }; arr.Length == 3
        return arr.Length;
    }

    // Arrays are REFERENCE types (like objects in JS / lists in Python).
    // Mutations made inside this method are visible to the caller.
    public static void SetFirst(int[] arr, int value)
    {
        // e.g. arr = { 1, 2, 3 }, value = 99; after call: arr == { 99, 2, 3 }
        arr[0] = value;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON E: Out of bounds throws — there is no silent `undefined`
    // ─────────────────────────────────────────────────────────────
    // JS: arr[100] on a 5-element array returns `undefined` — quiet, easy
    //     to miss, possibly fatal later.
    // C#: throws IndexOutOfRangeException immediately. Loud, but at least
    //     you know about the bug on the line that caused it.

    // Gotcha — will throw at runtime:
    public static int AccessOutOfBounds(int[] arr)
    {
        // e.g. arr has 3 items, arr[100] → throws IndexOutOfRangeException
        return arr[100];
    }

    // Fix — check the index against .Length before using it:
    public static int SafeGet(int[] arr, int index, int fallback)
    {
        // e.g. arr = { 1, 2, 3 }, index = 10 → returns fallback
        //      arr = { 1, 2, 3 }, index = 1  → returns 2
        if (index < 0 || index >= arr.Length)
        {
            return fallback;
        }
        return arr[index];
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON F: Iteration — `foreach` vs `for`
    // ─────────────────────────────────────────────────────────────
    // Two common loop styles for arrays. Rule of thumb:
    //   • `foreach` — when you only need each VALUE.
    //   • `for`     — when you need the INDEX (to compare positions,
    //                 to mutate slots, to iterate every-other, etc.).

    // foreach — simpler, harder to get wrong (can't go out of bounds).
    public static int SumWithForeach(int[] arr)
    {
        // e.g. arr = { 1, 2, 3, 4 }; returns 10
        int total = 0;
        foreach (int n in arr)
        {
            total += n;
        }
        return total;
    }

    // for — you control the index. Useful when you need `i` itself.
    public static int SumWithFor(int[] arr)
    {
        // e.g. arr = { 1, 2, 3, 4 }; returns 10 (same result, different style)
        int total = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            total += arr[i];
        }
        return total;
    }

    // When you need the index — e.g. to write into each slot:
    public static int[] FillWithIndexSquares(int length)
    {
        // e.g. length = 4; returns { 0, 1, 4, 9 }
        int[] result = new int[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = i * i;
        }
        return result;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON G: Array utilities — static methods on the `Array` class
    // ─────────────────────────────────────────────────────────────
    // Array.Sort, Array.Reverse, Array.IndexOf are the most common utilities.
    //
    // GOTCHA: Sort and Reverse MUTATE the array in place — they do NOT
    // return a new sorted/reversed copy. If you need to keep the original,
    // make a copy first (we'll see cleaner ways to copy later).

    public static int[] SortInPlace(int[] arr)
    {
        // e.g. arr = { 3, 1, 2 }; after call: arr == { 1, 2, 3 }  (caller's array mutated)
        Array.Sort(arr);
        return arr;
    }

    public static int[] ReverseInPlace(int[] arr)
    {
        // e.g. arr = { 1, 2, 3 }; after call: arr == { 3, 2, 1 }
        Array.Reverse(arr);
        return arr;
    }

    public static int FindIndexOf(int[] arr, int value)
    {
        // e.g. arr = { 10, 20, 30 }, value = 20 → returns 1
        //      arr = { 10, 20, 30 }, value = 99 → returns -1 (not found)
        return Array.IndexOf(arr, value);
    }

    // ─────────────────────────────────────────────────────────────
    // Closing note
    // ─────────────────────────────────────────────────────────────
    // Arrays are FIXED SIZE. If you need to add or remove elements after
    // creation, use List<T> — covered in the next theme.
}
