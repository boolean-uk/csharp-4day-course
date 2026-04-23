namespace OopSandbox;

// Reusable across Examples 1–4 of lesson_plans/oop.md. Same shape as the
// struct students wrote in Fundamentals/Exercises/Structs.cs, so you can
// reference it verbally if it helps. Kept as a value type on purpose —
// Example 3 passes one into a helper to show struct pass-by-value.
public struct Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X}, {Y})";
}
