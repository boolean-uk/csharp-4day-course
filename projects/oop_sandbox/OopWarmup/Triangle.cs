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
        get
        {
            double ab = A.DistanceTo(B);
            double bc = B.DistanceTo(C);
            double ca = C.DistanceTo(A);
            double s = (ab + bc + ca) / 2;
            return Math.Sqrt(s * (s - ab) * (s - bc) * (s - ca));
        }
    }

    public override double Perimeter => A.DistanceTo(B) + B.DistanceTo(C) + C.DistanceTo(A);
}
