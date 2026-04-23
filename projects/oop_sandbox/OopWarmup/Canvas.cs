namespace OopWarmup;

// Holds a collection of Shapes. Your job is EXERCISE 4 (FindLargest).
public class Canvas
{
    private List<Shape> shapes = new List<Shape>();

    public void Add(Shape s) => shapes.Add(s);
    public int Count => shapes.Count;

    public double TotalArea
    {
        get
        {
            double total = 0;
            foreach (Shape s in shapes) total += s.Area;
            return total;
        }
    }

    public void DescribeAll()
    {
        foreach (Shape s in shapes) Console.WriteLine(s.Describe());
    }

    // EXERCISE 4: FindLargest
    // Return the Shape on this canvas with the greatest Area.
    // Return null if the canvas is empty.
    //
    // The test asserts with Assert.Same — you must return the SAME reference
    // that was added, not a new or copied shape.
    public Shape? FindLargest()
    {
        throw new NotImplementedException("TODO: iterate shapes, return the one with max Area; null if empty");
    }
}
