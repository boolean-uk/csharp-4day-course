# 90-minute Lesson — Object-Oriented Programming Deep Dive

Running domains:

- **`Vector2D`** for Examples 1–4 (single class, encapsulation, constructors,
  reference vs value). The compelling angle: we store a vector two genuinely
  different ways (Cartesian endpoint vs polar `(magnitude, angle)`) and show
  the public API survives the swap.
- **`Shape` / `Circle` / `Rectangle`** for Example 5 (inheritance and
  polymorphism) — vectors don't have a natural IS-A hierarchy; shapes do.
- **`Canvas`** and a tiny **`Box<T>`** for Example 6 (generics).

Aim by end of the hour-and-a-half:

- Students know what a class is, why we make objects, and what visibility
  buys us (Block B1, B2).
- They can explain reference vs value semantics without hesitating (B3).
- They can write a constructor with validation and understand overloads +
  static factory methods (B4).
- They can read and write a class hierarchy that uses `virtual` / `override`
  (B5).
- They understand `List<T>` enough to be productive and know `<T>` is a type
  placeholder they'll see everywhere in .NET (B6).
- They're set up to start the Bank capstone (Block C).

---

## Time budget

| Block | Mins | What happens |
|---|---|---|
| A | 20 | Open Q&A + diagnostic recap questions |
| B | 60 | Live-coded OOP examples — Vector2D then Shape/Canvas |
| C | 10 | Brief students on exercises, then hand off |

---

## Block A — Q&A + diagnostic recap (20 min)

First **10 min**: open the floor for questions about anything from the
fundamentals work so far.

Second **10 min**: you ask them. Below are diagnostic questions calibrated at
spots Python/JS programmers typically gloss over. Pick 5–6 based on where
you've seen them stumble. Each question has a **drop-in code block** you can
paste into a scratch `Program.cs` and run with `dotnet run` if the answer
needs a live demo.

### 1. Integer division

*"`7 / 2` in C# is `3`. Why, and how do you get `3.5`?"*

Integer `/` integer = integer; the fractional part is truncated. Fix: cast at
least one side to `double`.

```csharp
int a = 7;
int b = 2;
Console.WriteLine(a / b);             // 3 — fractional part truncated

Console.WriteLine((double)a / b);     // 3.5 — cast forces floating-point division
Console.WriteLine(a / (double)b);     // 3.5 — same deal, other side
Console.WriteLine(7.0 / 2);           // 3.5 — one double literal is enough
```

### 2. `char` arithmetic

*"What TYPE is `'a' + 'b'`?"*

`int`, not a string. `char + char` does *numeric* arithmetic on Unicode code
points.

```csharp
char a = 'a';  // Unicode 97
char b = 'b';  // Unicode 98
var sum = a + b;
Console.WriteLine(sum);                 // 195
Console.WriteLine(sum.GetType());       // System.Int32 — it's an int!

// To concatenate as text you need strings:
Console.WriteLine("a" + "b");           // "ab"
Console.WriteLine($"{a}{b}");           // "ab" — interpolation boxes chars as strings
```

### 3. String immutability

*"I write `s.ToUpper();` on a line by itself. What happens to `s`?"*

Nothing. Strings are immutable; `ToUpper()` returns a **new** string, and if
you don't capture it, it's gone.

```csharp
string s = "hello";

s.ToUpper();                // return value thrown away
Console.WriteLine(s);       // "hello" — unchanged

s = s.ToUpper();            // capture the NEW string
Console.WriteLine(s);       // "HELLO"
```

### 4. Array out of bounds

*"`arr[100]` on a 5-element array — what happens?"*

`IndexOutOfRangeException`. No silent `undefined` like JavaScript — C# fails
loudly the moment you do it.

```csharp
int[] arr = { 1, 2, 3, 4, 5 };
try
{
    int x = arr[100];
    Console.WriteLine(x);       // never runs
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

// Safe pattern — check first:
int idx = 100;
if (idx >= 0 && idx < arr.Length)
    Console.WriteLine(arr[idx]);
else
    Console.WriteLine($"Index {idx} is out of range");
```

### 5. `switch` fall-through

*"Does a `case` fall through to the next `case` by default in C#?"*

**No.** C# requires `break` (or `return` / `throw`) and the compiler errors
if you forget. If you want fall-through, stack case labels or use
`goto case` explicitly.

```csharp
int day = 6;

switch (day)
{
    // ✓ Stacked case labels (empty ones) DO share a body — this is idiomatic.
    case 1:
    case 2:
    case 3:
    case 4:
    case 5:
        Console.WriteLine("Weekday");
        break;
    case 6:
    case 7:
        Console.WriteLine("Weekend");
        break;
    default:
        Console.WriteLine("Invalid");
        break;
}

// ✗ The C-style "silent fall-through" does NOT compile in C#:
//
//   case 1:
//       Console.WriteLine("One");
//       // No break! → CS0163: "Control cannot fall through from one case
//       //              label ('case 1:') to another."
//   case 2:
//       Console.WriteLine("Two");
//       break;

// ✓ If you truly want fall-through, be explicit with `goto case`:
int n = 1;
switch (n)
{
    case 1:
        Console.WriteLine("One");
        goto case 2;     // jump into case 2's body
    case 2:
        Console.WriteLine("Two");
        break;
}
// Prints: One, Two
```

### 6. Struct vs class copy/share

*"If `b = a;` and you change `b.X`, did `a` change? Struct vs class?"*

Struct no (copy); class yes (shared reference).

```csharp
// ── STRUCT: b = a copies ─────────────────────────────────────
PointS sa = new PointS(1, 2);
PointS sb = sa;                 // COPY — independent
sb.X = 99;
Console.WriteLine($"Struct:  a.X={sa.X}, b.X={sb.X}");   // a.X=1, b.X=99

// ── CLASS: b = a shares ──────────────────────────────────────
PointC ca = new PointC(1, 2);
PointC cb = ca;                 // SAME reference
cb.X = 99;
Console.WriteLine($"Class:   a.X={ca.X}, b.X={cb.X}");   // a.X=99, b.X=99

// Types go at the bottom of the file in a top-level Program.cs
public struct PointS { public int X; public int Y; public PointS(int x, int y) { X = x; Y = y; } }
public class  PointC { public int X; public int Y; public PointC(int x, int y) { X = x; Y = y; } }
```

### 7. Struct vs class, how to choose

*"When would you pick a struct over a class?"*

Struct: small, value-like data with no identity — you care about the
*contents*. Class: anything with identity, lifecycle, or behaviour — you care
about *which* object it is.

```csharp
// STRUCT — small, value-like, no identity.
// £100 GBP is £100 GBP wherever you see it. Two Money values with the same
// amount + currency are effectively "the same". Copy freely.
public struct Money
{
    public decimal Amount { get; }
    public string Currency { get; }
    public Money(decimal amount, string currency) { Amount = amount; Currency = currency; }
}

// CLASS — identity matters.
// Two User objects with the same Name are STILL different users: different
// CreatedAt, different login history, different "which row in the DB".
public class User
{
    public string Name { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}

Money a = new Money(100m, "GBP");
Money b = new Money(100m, "GBP");
// a and b represent the same money — equivalent for every practical purpose.

User u1 = new User { Name = "Ada", CreatedAt = DateTime.UtcNow };
User u2 = new User { Name = "Ada", CreatedAt = DateTime.UtcNow };
// u1 and u2 are DIFFERENT users even with matching fields.
Console.WriteLine(object.ReferenceEquals(u1, u2));  // False
```

Other rules of thumb: struct if the data is **≤16 bytes**, immutable, and
short-lived; class for anything that **inherits**, anything with a **long
lifetime**, anything you'll pass around and mutate.

### 8. `decimal` literals

*"`decimal d = 1.5;` — will this compile?"*

No — `1.5` is a `double` literal and C# won't silently narrow a `double` to a
`decimal` (precision loss). Use the `m` suffix.

```csharp
// decimal d1 = 1.5;
//            ^^^  CS0664: Literal of type double cannot be implicitly
//                 converted to type 'decimal'; use an 'M' suffix to create
//                 a literal of this type.

decimal d2 = 1.5m;         // ✓ 'm' (or 'M') — decimal literal
Console.WriteLine(d2);     // 1.5

decimal d3 = 1;            // ✓ int literal implicitly widens to decimal (no precision loss)
Console.WriteLine(d3);     // 1

// Same rule everywhere decimals appear — method args, array literals, etc.
decimal[] prices = { 9.99m, 19.95m, 0.5m };
```

### 9. Default values for uninitialised fields

*"What does an uninitialised reference-type field default to? And a value-type field?"*

Reference types → `null`. Value types → that type's "zero" value.

```csharp
public class Defaults
{
    public string? Text;     // reference type — null
    public int Number;       // value type — 0
    public bool Flag;        // value type — false
    public DateTime When;    // value-type struct — DateTime.MinValue (01/01/0001)
    public int[]? Array;     // reference type — null
}

Defaults d = new Defaults();
Console.WriteLine($"Text:   {d.Text ?? "<null>"}");          // <null>
Console.WriteLine($"Number: {d.Number}");                    // 0
Console.WriteLine($"Flag:   {d.Flag}");                      // False
Console.WriteLine($"When:   {d.When}");                      // 01/01/0001 00:00:00
Console.WriteLine($"Array:  {(d.Array is null ? "<null>" : "assigned")}"); // <null>
```

### 10. Throwing in a constructor

*"If I throw inside a constructor, does the object get created?"*

No. Construction aborts; the caller's variable is never assigned. Catch the
exception the way you would any other.

```csharp
public class Age
{
    public int Years { get; }
    public Age(int years)
    {
        if (years < 0)
            throw new ArgumentException("Years cannot be negative.", nameof(years));
        Years = years;
    }
}

Age? person = null;
try
{
    person = new Age(-5);             // throws
    Console.WriteLine("This line never runs");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

Console.WriteLine($"person is {(person is null ? "null" : "assigned")}");
// → null: the assignment never happened because the constructor threw.
```

### Hot spots to revisit live

If several students got the same one wrong, pull up the relevant fundamentals
file:

- Struct vs class copy/share → `Fundamentals/Lessons/StructsVsClasses.cs` and
  walk the `PointStruct` / `PointClass` pair.
- Immutability gotcha → `Fundamentals/Lessons/Strings.cs` Lesson C.
- Switch break-flip → `Fundamentals/Lessons/ControlFlow.cs` Lesson D.

---

## Block B — Live OOP coding (60 min)

**Setup**: one blank `.cs` file, shared on screen. Build everything
incrementally — don't paste finished code. Timings are targets, not
contracts.

A bare `Point` struct is our building block for the Vector2D examples. Paste
this to start, or reference the one students already wrote in the struct
exercise:

```csharp
public struct Point
{
    public double X { get; set; }
    public double Y { get; set; }
    public Point(double x, double y) { X = x; Y = y; }
    public override string ToString() => $"({X}, {Y})";
}
```

(`override string ToString()` is a tiny polymorphism teaser you can namecheck
but defer explanation on until Example 5.)

---

### Example 1 — What a class is + visibility + a real refactor (12 min)

Start with `Vector2D` **the wrong way** — public fields:

```csharp
public class Vector2D
{
    public double X;
    public double Y;
}
```

Instantiate it:

```csharp
Vector2D v = new Vector2D();
v.X = 3;
v.Y = 4;
Console.WriteLine($"Magnitude = {Math.Sqrt(v.X * v.X + v.Y * v.Y)}");
```

**Ask**: *"What's wrong with this?"* Pull three answers:

1. **No guarantee**. Nothing stops `v.X = double.NaN`.
2. **No behaviour**. Every caller has to redo the `√(x² + y²)` formula for the
   magnitude.
3. **No encapsulation**. If we want to store the vector differently later —
   say in polar form — every caller that reads `v.X` / `v.Y` directly breaks.

Rewrite it as the **right way** — the Cartesian version:

```csharp
public class Vector2D
{
    private Point endpoint;   // vector from origin (0,0) to endpoint

    public Vector2D(double x, double y)
    {
        endpoint = new Point(x, y);
    }

    public double X         => endpoint.X;
    public double Y         => endpoint.Y;
    public double Magnitude => Math.Sqrt(X * X + Y * Y);
    public double Angle     => Math.Atan2(Y, X);   // radians from +x axis
}
```

Use it:

```csharp
Vector2D v = new Vector2D(3, 4);
Console.WriteLine($"X = {v.X}, Y = {v.Y}");
Console.WriteLine($"Magnitude = {v.Magnitude}");                    // 5
Console.WriteLine($"Angle = {v.Angle:F3} rad " +
                  $"({v.Angle * 180 / Math.PI:F1} deg)");           // ~0.927 rad (~53.1°)
```

**Points to hit:**

- `class Name { ... }` declaration.
- `private` field = internal state, hidden from callers.
- Public **properties** expose read-only computed values.
- Expression-bodied getters (`=>`) are the same thing as `{ get { return …; } }`.
- Constructor puts the object into a valid state once; afterwards the caller
  can trust it.

**The payoff — swap the internal representation to polar.** Same public
contract, totally different innards:

```csharp
public class Vector2D
{
    // Polar storage: magnitude + angle, no Point in sight.
    private double magnitude;
    private double angle;   // radians from +x axis

    public Vector2D(double x, double y)
    {
        magnitude = Math.Sqrt(x * x + y * y);
        angle     = Math.Atan2(y, x);
    }

    public double X         => magnitude * Math.Cos(angle);
    public double Y         => magnitude * Math.Sin(angle);
    public double Magnitude => magnitude;
    public double Angle     => angle;
}
```

**The call site does NOT change.** `new Vector2D(3, 4)` still works,
`v.Magnitude` still reads `5` (within floating-point tolerance), `v.X` still
reads `3`. Callers don't know or care that the class now stores a magnitude
and an angle instead of a `Point`.

**Say it out loud.** *"Because the fields were `private`, we ripped out the
storage scheme and no caller noticed. If the fields had been `public`,
everyone reading `v.endpoint` would now have a compile error against a field
that doesn't exist."*

This is the whole argument for encapsulation: **you keep the freedom to
change your mind about implementation**.

> **Why is polar a real choice, not a contrivance?** Polar storage is the
> natural choice when the operations you do most are *rotation* and *scaling*
> (both are trivial in polar, messy in Cartesian). Cartesian wins when you do
> *addition* (trivial in Cartesian, messy in polar). Real code picks the
> storage that matches the hot path — which is exactly why encapsulation
> matters, because the hot path might change.

---

### Example 2 — Many instances, each with its own state (5 min)

Back to the Cartesian version. Instantiate three:

```csharp
Vector2D east  = new Vector2D(1, 0);
Vector2D north = new Vector2D(0, 1);
Vector2D diag  = new Vector2D(3, 4);

Console.WriteLine($"east:  magnitude={east.Magnitude},  angle={east.Angle:F2}");
Console.WriteLine($"north: magnitude={north.Magnitude}, angle={north.Angle:F2}");
Console.WriteLine($"diag:  magnitude={diag.Magnitude},  angle={diag.Angle:F2}");
```

**Points to hit:**

- Each `new Vector2D(...)` produces a *fresh* object — its own `endpoint`,
  independent of the others.
- Properties and methods operate on **this** instance's fields.
- No static state — every field lives *inside* the object.
- Three objects on the heap, three independent lifetimes.

> **Gotcha to name**: `Vector2D v1 = new Vector2D(1, 0); Vector2D v2 = v1;`
> does **not** make two vectors — it makes two names for one vector. We'll
> prove that next.

---

### Example 3 — Reference vs value, nailed down (8 min)

Add an in-place mutator to `Vector2D` so we have something to see change:

```csharp
public void ScaleBy(double factor)
{
    // In the Cartesian rep, scale both components.
    endpoint = new Point(endpoint.X * factor, endpoint.Y * factor);
}
```

Now demonstrate **passing a class**:

```csharp
void DoubleIt(Vector2D v) => v.ScaleBy(2);

Vector2D v = new Vector2D(3, 4);
Console.WriteLine($"Before: magnitude = {v.Magnitude}");  // 5
DoubleIt(v);
Console.WriteLine($"After:  magnitude = {v.Magnitude}");  // 10 — CALLER SAW THE CHANGE
```

Then demonstrate **passing a struct** (our `Point`):

```csharp
void ShiftPoint(Point p) => p = new Point(p.X + 10, p.Y);

Point p = new Point(0, 0);
ShiftPoint(p);
Console.WriteLine(p);     // (0, 0) — CALLER DID NOT SEE THE CHANGE
```

**Points to hit:**

- Classes → reference semantics. `v` inside `DoubleIt` refers to the same
  object the caller holds. Mutations leak out.
- Structs → value semantics. `p` inside `ShiftPoint` is a *copy*. Mutations
  are lost.
- Callback to `Fundamentals/Lessons/StructsVsClasses.cs` — this is that exact
  lesson made concrete in a new domain.
- One-line rule of thumb: *"If you wrote `class`, passing it shares. If you
  wrote `struct`, passing it copies."*

**Honesty aside (one-liner — say it, don't dwell):** *"In real .NET code,
2D vectors are usually **structs** (see `System.Numerics.Vector2`) because
mathematically they're values, not identities. We're using a `class` here
to illustrate reference semantics — don't take this as production guidance."*

---

### Example 4 — Constructor overloading + chaining + a static factory (12 min)

Add extra constructors to `Vector2D`, all chaining to the canonical one.

```csharp
public class Vector2D
{
    private Point endpoint;

    // Canonical constructor — the one that does validation and real assignment.
    public Vector2D(double x, double y)
    {
        if (double.IsNaN(x) || double.IsNaN(y))
            throw new ArgumentException("Components must be finite numbers.");
        endpoint = new Point(x, y);
    }

    // Convenience: zero vector.
    public Vector2D() : this(0, 0) { }

    // Convenience: build from a Point directly.
    public Vector2D(Point endpoint) : this(endpoint.X, endpoint.Y) { }

    // ... X, Y, Magnitude, Angle, ScaleBy as before
}
```

Demonstrate the overloads:

```csharp
Vector2D zero     = new Vector2D();                      // (0, 0)
Vector2D explicit_= new Vector2D(3, 4);                  // (3, 4)
Vector2D fromPt   = new Vector2D(new Point(1, 2));       // (1, 2)

// Validation runs no matter which constructor was called:
try
{
    Vector2D bad = new Vector2D(double.NaN, 0);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}
```

**Points to hit:**

- **Overloading** = same name, different parameter lists. C# picks the right
  one at compile time.
- `: this(...)` delegates to another constructor of the **same** class. DRY —
  validation lives in one place.
- Throwing in a constructor aborts construction (callback to Q10 earlier).

**Now the limit — and the fix: a static factory.** We can't add another
overload for polar input, because `Vector2D(double, double)` is already
taken by Cartesian — the compiler has no way to know which you mean:

```csharp
// public Vector2D(double magnitude, double angle) { ... }
// ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
// CS0111: Type 'Vector2D' already defines a member called '.ctor' with the
//         same parameter types.
```

The common fix: a **static factory method** with a name that tells the
caller what they're passing.

```csharp
public static Vector2D FromPolar(double magnitude, double angle)
{
    if (magnitude < 0)
        throw new ArgumentException("Magnitude must be non-negative.", nameof(magnitude));
    return new Vector2D(magnitude * Math.Cos(angle), magnitude * Math.Sin(angle));
}
```

Use it:

```csharp
Vector2D north = Vector2D.FromPolar(1, Math.PI / 2);    // (0, 1)
Vector2D unit45 = Vector2D.FromPolar(1, Math.PI / 4);   // (~0.707, ~0.707)
```

**Say:** *"When overloading hits its limit — two constructors with the same
parameter types mean different things — a named `static` factory reads
clearly at the call site and delegates to the canonical constructor."*

> **If they ask**: *"Can methods be overloaded too, not just constructors?"*
> Yes — same rules. `Math.Abs` has eleven overloads, one per numeric type.

---

### Example 5 — Inheritance + polymorphism (with Shapes) (15 min)

**Segue:** *"So far every example has been one class. Sometimes you need a
family of related types that share some behaviour and differ in the rest.
That's what inheritance models. Vectors don't have a natural family; shapes
do."*

Introduce `Shape` as an **abstract base**:

```csharp
public abstract class Shape
{
    public abstract double Area { get; }
    public abstract double Perimeter { get; }

    public virtual string Describe()
        => $"{GetType().Name} — area {Area:F2}, perimeter {Perimeter:F2}";
}
```

Add `Circle`:

```csharp
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
```

Add a sibling — `Rectangle`:

```csharp
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

    // Extend, don't replace — we still want the base Describe's content.
    public override string Describe() => $"{base.Describe()} [{width} x {height}]";
}
```

Now polymorphism:

```csharp
Shape[] shapes = {
    new Circle(new Point(0, 0), 3),
    new Rectangle(new Point(0, 0), 4, 5),
    new Circle(new Point(10, 10), 1)
};

foreach (Shape s in shapes)
    Console.WriteLine(s.Describe());

double totalArea = 0;
foreach (Shape s in shapes) totalArea += s.Area;
Console.WriteLine($"Total area: {totalArea:F2}");
```

**Points to hit:**

- `: Shape` after the class name declares inheritance. `Circle` inherits
  everything `Shape` declares.
- `abstract class Shape` — you can't `new Shape()`; `Shape` only exists as a
  base. Enforces "Shape is a category, not a concrete thing".
- `abstract double Area { get; }` — no body; each subclass **must** provide
  one. Compare with `virtual` (has a default, subclasses **may** override).
- `override` on the subclass declares its version.
- `base.Describe()` = call the parent's implementation. `Rectangle.Describe`
  uses it to extend rather than replace.
- **Polymorphism**: a `Shape[]` holds `Circle` and `Rectangle` instances.
  `s.Describe()` and `s.Area` dispatch to the right override at runtime.
- Python analogue: this is exactly what method resolution in a Python class
  hierarchy does. C# makes `abstract` / `virtual` / `override` explicit in
  the syntax.

> **The "IS-A" heuristic**: if you can say *"a Circle is a Shape"*,
> inheritance fits. If you have to say *"a Car **has an** Engine"*, use
> composition (a field) instead. Both patterns exist. Inheritance is not the
> default; reach for composition first.

---

### Example 6 — Generics primer (8 min)

Two pieces: one using `List<T>` (they've seen it); one defining a tiny
generic of our own.

#### 6a. `List<Shape>` via a `Canvas` class

```csharp
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
}

Canvas canvas = new Canvas();
canvas.Add(new Circle(new Point(0, 0), 3));
canvas.Add(new Rectangle(new Point(0, 0), 4, 5));
canvas.DescribeAll();
Console.WriteLine($"Total area: {canvas.TotalArea:F2}");
```

**Points to hit:**

- `List<Shape>` means *"a list where every element is a `Shape` (or a
  subclass of it)"*. Compile-time type safety — you can't `canvas.Add("oops")`.
- Contrast with Python's `list` or JS's array: those accept anything at
  runtime.
- `List<T>` is in the standard library; `T` is the type the list holds.

#### 6b. Writing your own generic — `Box<T>`

Smallest possible generic, just to show the syntax:

```csharp
public class Box<T>
{
    private T contents;

    public Box(T contents) { this.contents = contents; }

    public T Peek() => contents;
    public void Replace(T newContents) => contents = newContents;
}

Box<int>       ib = new Box<int>(42);
Box<string>    sb = new Box<string>("hello");
Box<Vector2D>  vb = new Box<Vector2D>(new Vector2D(3, 4));
Box<Circle>    cb = new Box<Circle>(new Circle(new Point(0, 0), 1));

Console.WriteLine(ib.Peek());                // 42
Console.WriteLine(sb.Peek());                // hello
Console.WriteLine(vb.Peek().Magnitude);      // 5
Console.WriteLine(cb.Peek().Area);           // ~3.14
```

**Points to hit:**

- `<T>` is a **type placeholder**. The caller fills it in at usage.
- Every `Box<T>` is a distinct type to the compiler — `Box<int>` and
  `Box<string>` don't mix.
- This is how `List<T>`, `Dictionary<TKey, TValue>`, `Nullable<T>`, and half
  the BCL are built.
- They'll mostly **consume** generics, not write them. That's fine.

> **If they ask**: *"Can I constrain T?"* Yes — `class Box<T> where T : Shape`
> would restrict `T` to a `Shape`. Don't derail the lesson; mention only if
> someone asks.

---

### Block B wrap-up (2 min)

Put it all together verbally at the whiteboard:

1. A **class** bundles state (private fields) with behaviour (public methods).
2. A **constructor** makes objects valid on creation; if it throws, no object
   is produced.
3. **Overloading** offers multiple ways to call the same thing; constructor
   chaining keeps the validation DRY. When overloading runs out (two
   meanings, same parameter types), reach for a **static factory method**.
4. **Inheritance** models "IS-A". `abstract` forces subclasses to fill in
   behaviour; `virtual` lets them override a default.
5. **Polymorphism** means one variable of a base type can hold any subclass —
   the right method runs at runtime.
6. **Generics** (`List<T>`, `Box<T>`, ...) give us one shape that works for
   many types, with compile-time type safety.

Encapsulation is the thread running through all of it: **private by default,
public only when you mean it**.

---

## Block C — Brief + exercises (10 min)

Hand the class off to the Bank capstone (`projects/bank/`):

- Open `projects/bank/README.md` on the projector. Walk through the spec
  tables (Transaction, Account, Bank). 2 min.
- Show the two mini-lessons in that README — `decimal` and DateTime — and
  flag that they'll refer back while working. 2 min.
- Show the test list in Test Explorer (all red). Suggested test order is
  printed in the README. 1 min.
- Kick them off. 5 min remaining for last questions while they set up.

### Planned additions to the Bank capstone

If you're happy with them, I'll add the following extensions to
`projects/bank/README.md`. Each maps to something we'll have just taught:

1. **Statement by date range** — `Statement(DateTime from, DateTime to)`
   overload. Exercises **method overloading** + DateTime filtering.
2. **Categorised transactions** — a `TransactionCategory` enum (Food, Rent,
   Salary, Transfer, …) and a `FindTransactions(TransactionCategory)`
   overload. More **enums** + **overloading**.
3. **Generic ledger** — write a `Ledger<T>` that the Account uses internally.
   Exercises **writing** custom generics, not just consuming `List<T>`.
4. **Custom exception type** — replace raw `InvalidOperationException` with
   `InsufficientFundsException : Exception`, carrying `RequestedAmount` and
   `AvailableBalance` public properties. Exercises **custom exceptions** +
   **inheritance from Exception**.
5. **Account type hierarchy** — `SavingsAccount` (earns interest, no
   overdraft) vs `CurrentAccount` (overdraft allowed, no interest), both
   deriving from `Account`. Exercises **inheritance** and **virtual /
   override** in the students' own code. (Already mentioned in extensions;
   I'll sharpen the spec.)

Say the word and I'll update the Bank README with these.

---

## Appendix — if you have spare time

Handy extensions to the live lesson if you finish early or want to fill
between blocks:

- **Equality.** `new Vector2D(3, 4) == new Vector2D(3, 4)` is `false` by
  default — `==` on reference types compares references, not contents.
  Bridge to `record class`, `Equals`, `GetHashCode` — don't implement; just
  flag for later.
- **`readonly` fields.** `private readonly Point endpoint;` and watch the
  compiler block reassignment outside the constructor. Reinforces the
  immutability mental model.
- **`sealed`.** `sealed class Circle : Shape` can't be inherited from. Useful
  when you want to close the hierarchy.
- **`ToString()` override.** Override `ToString()` on `Vector2D` or `Shape`,
  show `Console.WriteLine(v)` calling it automatically. Namecheck that every
  type in .NET inherits from `object` and all the methods that come with.
