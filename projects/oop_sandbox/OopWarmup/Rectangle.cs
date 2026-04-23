namespace OopWarmup;

public class Rectangle : Shape
{
    private Point bottomLeft;
    private double width;
    private double height;

    public Rectangle(Point bottomLeft, double width, double height)
    {
        if (width <= 0 || height <= 0)
            throw new ArgumentException("Dimensions must be positive.");
        this.bottomLeft = bottomLeft;
        this.width = width;
        this.height = height;
    }

    public override double Area      => width * height;
    public override double Perimeter => 2 * (width + height);

    public override string Describe() => $"{base.Describe()} [{width} x {height}]";
}
