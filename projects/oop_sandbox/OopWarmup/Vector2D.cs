namespace OopWarmup;

// Same Vector2D your instructor live-coded in class — Cartesian internal
// storage, public properties for X / Y / Magnitude / Angle, and a ScaleBy
// mutator for the reference-vs-value demo.
//
// Your job is EXERCISE 1 (DotProduct) and EXERCISE 2 (ToString override).
public class Vector2D
{
    private Point endpoint;

    public Vector2D(double x, double y)
    {
        if (double.IsNaN(x) || double.IsNaN(y))
            throw new ArgumentException("Components must be finite numbers.");
        endpoint = new Point(x, y);
    }

    public double X => endpoint.X;
    public double Y => endpoint.Y;
    public double Magnitude => Math.Sqrt(X * X + Y * Y);
    public double Angle => Math.Atan2(Y, X);

    public Vector2D Add(Vector2D other) => new Vector2D(X + other.X, Y + other.Y);

    public void ScaleBy(double factor)
    {
        endpoint = new Point(endpoint.X * factor, endpoint.Y * factor);
    }

    // EXERCISE 1: DotProduct
    // Return the dot product of THIS vector and `other`.
    // Formula: (this.X * other.X) + (this.Y * other.Y)
    // See the README for the exercise brief and a Wikipedia link.
    public double DotProduct(Vector2D other)
    {
        return X * other.X + Y * other.Y;
    }

    // EXERCISE 2: ToString override
    // Make `Console.WriteLine(v)` print "[3, 4]" for a vector with X=3, Y=4.
    // You'll override the ToString method inherited from `object`.
    // Every type in C# inherits from object — see the README link.
    public override string ToString()
    {
        return $"[{X}, {Y}]";
    }
}
