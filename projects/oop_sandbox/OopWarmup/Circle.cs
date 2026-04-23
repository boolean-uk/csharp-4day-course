namespace OopWarmup;

public class Circle : Shape
{
    private Point center;
    private double radius;

    public Circle(Point center, double radius)
    {
        if (radius <= 0)
            throw new ArgumentException("Radius must be positive.", nameof(radius));
        this.center = center;
        this.radius = radius;
    }

    public override double Area      => Math.PI * radius * radius;
    public override double Perimeter => 2 * Math.PI * radius;

    public Point  Center => center;
    public double Radius => radius;
}
