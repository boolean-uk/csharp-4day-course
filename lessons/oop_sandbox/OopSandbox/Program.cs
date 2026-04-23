// ═══════════════════════════════════════════════════════════════════════
// OOP lesson sandbox
// ═══════════════════════════════════════════════════════════════════════
//
// Live-code here during Block B of lesson_plans/oop.md.
//
// What's ready for you:
//   • Point.cs                 — the reusable Point struct (Examples 1–4).
//   • Top-level statements     — type new demo code right below.
//   • Types-after-top-level OK — class/struct declarations go at the bottom
//                                of this file, or add Vector2D.cs / Shape.cs
//                                / Circle.cs etc. as separate files next to
//                                this one.
//   • <Using Include="OopSandbox" /> in the csproj — no `using` statements
//     needed to reach Point / Vector2D / Shape. Everything just resolves.
//
// Block A — diagnostic questions: paste the snippet for whichever question
// you want to demo over this file (or make a second sandbox). Each snippet
// in oop.md is self-contained.
//
// Block B — live examples: suggested flow is below. Delete / uncomment as
// you work through.
//
// Run with `dotnet run` from projects/oop_sandbox/OopSandbox, or press F5
// in Visual Studio with OopSandbox.slnx open.
// ═══════════════════════════════════════════════════════════════════════

Console.WriteLine("=== OOP sandbox ready ===");

// Sanity check — Point.cs is reachable with zero ceremony.
// Delete this line once you're into Example 1.
Point sanity = new Point(3, 4);
Console.WriteLine($"Point sanity check: {sanity}  (magnitude → {Math.Sqrt(sanity.X * sanity.X + sanity.Y * sanity.Y)})");


// ─── Example 1 — Vector2D + encapsulation ─────────────────────────────
// Uncomment once you've defined Vector2D (below, or in Vector2D.cs).
//
// Vector2D v = new Vector2D(3, 4);
// Console.WriteLine($"v.X = {v.X}, v.Y = {v.Y}");
// Console.WriteLine($"v.Magnitude = {v.Magnitude}");
// Console.WriteLine($"v.Angle = {v.Angle:F3} rad ({v.Angle * 180 / Math.PI:F1} deg)");


// ─── Example 3 — reference vs value ───────────────────────────────────
// Needs Vector2D with a ScaleBy(double) mutator.
//
// static void DoubleIt(Vector2D v) => v.ScaleBy(2);
// static void ShiftPoint(Point p) => p = new Point(p.X + 10, p.Y);
//
// Vector2D vRef = new Vector2D(3, 4);
// DoubleIt(vRef);
// Console.WriteLine($"After DoubleIt: magnitude = {vRef.Magnitude}"); // 10 — caller saw it
//
// Point pVal = new Point(0, 0);
// ShiftPoint(pVal);
// Console.WriteLine($"After ShiftPoint: {pVal}");                     // (0, 0) — unchanged


// ─── Example 5 — Shape hierarchy + polymorphism ───────────────────────
// Needs Shape (abstract), Circle : Shape, Rectangle : Shape.
//
// Shape[] shapes = {
//     new Circle(new Point(0, 0), 3),
//     new Rectangle(new Point(0, 0), 4, 5),
//     new Circle(new Point(10, 10), 1),
// };
// foreach (Shape s in shapes) Console.WriteLine(s.Describe());


// ─── Example 6 — Canvas + Box<T> ──────────────────────────────────────
// Needs Canvas and Box<T> defined.
//
// Canvas canvas = new Canvas();
// canvas.Add(new Circle(new Point(0, 0), 3));
// canvas.Add(new Rectangle(new Point(0, 0), 4, 5));
// canvas.DescribeAll();
// Console.WriteLine($"Total area: {canvas.TotalArea:F2}");
//
// Box<int>     ib = new Box<int>(42);
// Box<string>  sb = new Box<string>("hello");
// Console.WriteLine($"{ib.Peek()}  |  {sb.Peek()}");


// ─── FILESYSTEM LESSON — Block A: naive one-class starting point ──────
// See lesson_plans/filesystem.md § Block A. The class lives in FSNode.cs
// (loaded into this sandbox already, deliberately wrong).
//
// Uncomment to surface the pain points in class, before rewriting the
// class live as abstract FSNode + FileNode + DirectoryNode:
//
// FSNode root   = new FSNode("root")      { Children = new List<FSNode>() };
// FSNode src    = new FSNode("src")       { Children = new List<FSNode>() };
// FSNode readme = new FSNode("README.md") { SizeBytes = 1200 };
//
// root.Add(src);
// root.Add(readme);
// src.Add(new FSNode("Program.cs") { SizeBytes = 800 });
//
// Console.WriteLine($"root.Size()      = {root.Size()}");       // 2000 ✓
// Console.WriteLine($"root.FileCount() = {root.FileCount()}");  //    2 ✓
//
// // Pain point #1 — Add on a FILE compiles fine, explodes at runtime.
// // A good type system should make this impossible. Ours doesn't.
// try
// {
//     readme.Add(new FSNode("oops") { SizeBytes = 1 });
// }
// catch (NullReferenceException ex)
// {
//     Console.WriteLine($"readme.Add blew up: {ex.Message}");
// }
//
// // Pain point #2 — every method in FSNode.cs starts with
// // `if (Children == null)`. That `if` is the shape of a missing
// // subclass. Now rewrite as abstract FSNode + FileNode + DirectoryNode.
//
//
// ─── Types typed during the live lesson go below, or in new .cs files ──
//
// public class Vector2D { ... }
// public abstract class Shape { ... }
// public class Circle : Shape { ... }
// public class Rectangle : Shape { ... }
// public class Canvas { ... }
// public class Box<T> { ... }
//
// For the filesystem lesson, replace the class in FSNode.cs with:
//   public abstract class FSNode { ... }
//   public class FileNode : FSNode, ISearchable { ... }
//   public class DirectoryNode : FSNode, ISearchable { ... }
//   public interface ISearchable { FSNode? FindByName(string name); }
