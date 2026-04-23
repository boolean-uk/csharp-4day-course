namespace OopWarmup;

// Abstract base for a family of 2D shapes. Every concrete shape must
// supply Area and Perimeter. Describe() has a default that concrete
// shapes may override.
public abstract class Shape
{
    public abstract double Area { get; }
    public abstract double Perimeter { get; }

    public virtual string Describe()
        => $"{GetType().Name} — area {Area:F2}, perimeter {Perimeter:F2}";
}
