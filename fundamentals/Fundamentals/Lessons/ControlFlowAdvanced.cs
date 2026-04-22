namespace Fundamentals.Lessons;

// Theme: Control flow (advanced) — the switch EXPRESSION.
// Read Lessons/ControlFlow.cs first.
//
// This file contains the TEACHING EXAMPLES only. Your work lives in
// Exercises/ControlFlowAdvanced.cs.
public static class ControlFlowAdvanced
{
    // ─────────────────────────────────────────────────────────────
    // LESSON I: `switch` EXPRESSION (C# 8+)
    // ─────────────────────────────────────────────────────────────
    // Modern C# adds a switch *expression* — it RETURNS a value, unlike the
    // classic switch *statement* which only performs side effects. Closest
    // analogue: Python 3.10's `match`.
    //
    // Shape:
    //     var result = subject switch
    //     {
    //         pattern1 => value1,
    //         pattern2 => value2,
    //         _        => fallbackValue,   // `_` is the "anything else" catch-all
    //     };
    //
    // Each arm is `pattern => value`. Commas separate arms. No `case`, no
    // `break`, no colons. The whole thing is ONE expression — so you can
    // `return` it, assign it, or interpolate it.

    // Literal-case form — like the classic switch statement but as an expression.
    public static string DayNameShort(int day)
    {
        // e.g. DayNameShort(1) == "Mon"; DayNameShort(7) == "Sun"; DayNameShort(99) == "?"
        return day switch
        {
            1 => "Mon",
            2 => "Tue",
            3 => "Wed",
            4 => "Thu",
            5 => "Fri",
            6 => "Sat",
            7 => "Sun",
            _ => "?",
        };
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON J: Relational patterns
    // ─────────────────────────────────────────────────────────────
    // An arm's pattern can be a RELATIONAL comparison against a constant:
    //     >= 70    <= 0    > 100    < 18
    // That turns an else-if chain into one tidy switch expression.
    //
    // Arms are tested TOP-TO-BOTTOM — just like an else-if chain — so put
    // the most specific (highest threshold) arms first and the catch-all
    // last.

    // Same classifier as Lessons/ControlFlow.cs Grade, rewritten as an expression.
    public static string GradeFromMark(int mark)
    {
        // e.g. GradeFromMark(85) == "A"; GradeFromMark(63) == "B"; GradeFromMark(40) == "F"
        return mark switch
        {
            >= 70 => "A",
            >= 60 => "B",
            >= 50 => "C",
            _     => "F",
        };
    }

    // Patterns can be combined with `and` / `or` to express ranges or
    // alternatives in one arm:
    //     >= 0 and <= 15   — inclusive range
    //     "amber" or "yellow"  — either literal
    public static string TemperatureBand(int celsius)
    {
        // e.g. TemperatureBand(-5) == "freezing"; TemperatureBand(10) == "cold";
        //      TemperatureBand(20) == "warm";     TemperatureBand(30) == "hot"
        return celsius switch
        {
            < 0            => "freezing",
            >= 0 and <= 15 => "cold",
            > 15 and <= 25 => "warm",
            _              => "hot",
        };
    }
}
