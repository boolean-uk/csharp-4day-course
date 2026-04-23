namespace OopWarmup;

// EXERCISE 3: implement Area and Perimeter for a 3-vertex triangle.
//
// The constructor and vertex properties (A, B, C) are done for you — you
// just need the two computed properties. See the README for the spec,
// Heron's formula, and links.
public class Triangle : Shape
{
    public Point A { get; }
    public Point B { get; }
    public Point C { get; }

    public Triangle(Point a, Point b, Point c)
    {
        A = a;
        B = b;
        C = c;
    }

    public override double Area
    {
        // TODO: use Heron's formula. You'll need the side lengths first —
        // Point.DistanceTo is already implemented.
        get { throw new NotImplementedException("TODO: Heron's formula — see README link"); }
    }

    public override double Perimeter
    {
        // TODO: sum of the three side lengths.
        get { throw new NotImplementedException("TODO: sum of A-B, B-C, C-A distances"); }
    }
}
