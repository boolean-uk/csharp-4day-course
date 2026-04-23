# OOP warm-up exercises

Four small drills that apply what you saw in the OOP lesson. Do these
before moving on to the Bank capstone — they build confidence with the
mechanics (method on an instance, override, inheritance, polymorphism) in
bite-sized chunks so the bigger project feels familiar.

**What's in here**

```
projects/oop_sandbox/
├── OopSandbox.slnx          ← open this in Visual Studio
├── OopSandbox/              ← your instructor's live-code scratchpad
├── OopWarmup/               ← the warm-up code — you implement here
└── OopWarmup.Tests/         ← failing xUnit tests. Make them go green.
```

**How to work**

```sh
cd projects/oop_sandbox
dotnet test                  # every warm-up test is red initially
```

Or open `OopSandbox.slnx` in Visual Studio and use Test Explorer (Test →
Test Explorer) to run the tests and see which are red. Pick an exercise
below, open the file it points to, and replace the
`throw new NotImplementedException(...)` with real code until the
matching tests pass.

Expected baseline: **14 tests, 13 red, 1 green.** The green one
(`Triangle_IsA_Shape`) is a structural guard — it just confirms
`Triangle` is declared with `: Shape`, which is already done for you
in the stub. The other 13 are the ones you'll turn green.

---

# Exercise 1 — `Vector2D.DotProduct`

Implement `DotProduct(Vector2D other)` in `OopWarmup/Vector2D.cs`.

**Formula:** `this.X * other.X + this.Y * other.Y`

**Why it matters:** the dot product is the single most useful operation
on vectors — it tells you whether two vectors are parallel, perpendicular,
or point roughly the same way. You'll meet it again in graphics,
machine learning, physics.

**Tests:** `Vector2DTests.DotProduct_*` (three of them)

**Reference**

- [Dot product — Wikipedia](https://en.wikipedia.org/wiki/Dot_product)

---

# Exercise 2 — Override `Vector2D.ToString()`

Make `Console.WriteLine(v)` print `[3, 4]` for a vector with X=3, Y=4.

Every type in C# inherits from `System.Object`, which defines a
`ToString()` method that by default returns the type's full name. You
override it to produce something meaningful for your class.

The stub in `OopWarmup/Vector2D.cs` is marked with a TODO. Replace it
with a one-line expression body returning your formatted string.

**Tests:** `Vector2DTests.ToString_*` (two of them)

**Reference**

- [`object.ToString()`](https://learn.microsoft.com/en-us/dotnet/api/system.object.tostring)
- [How to override the `ToString` method (Microsoft Learn)](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-override-the-tostring-method)

---

# Exercise 3 — `Triangle : Shape`

Implement `Area` and `Perimeter` on `OopWarmup/Triangle.cs`. The
constructor and the vertex properties (`A`, `B`, `C`) are already done
for you.

**Perimeter** — sum of the three side lengths. The `Point` struct in
this project has a `DistanceTo(Point other)` method you can use. Three
calls, add them up.

**Area** — use **Heron's formula**:

```
s = (a + b + c) / 2            ← the semi-perimeter
area = √( s · (s - a) · (s - b) · (s - c) )
```

…where `a`, `b`, `c` are the three side lengths.

**Tests:** `TriangleTests.*` (five of them, including one that checks
`Describe()` works because of the `Shape` base class, with no extra work
from you — polymorphism in action).

**References**

- [Heron's formula — Wikipedia](https://en.wikipedia.org/wiki/Heron%27s_formula)
- [`Math.Sqrt` method](https://learn.microsoft.com/en-us/dotnet/api/system.math.sqrt)
- [`Math.Pow` method](https://learn.microsoft.com/en-us/dotnet/api/system.math.pow) — handy
  if you want `s*s*s*s` as `Math.Pow(s, 4)`, though plain multiplication is
  usually clearer here.

---

# Exercise 4 — `Canvas.FindLargest`

Implement `FindLargest()` in `OopWarmup/Canvas.cs`. Return the `Shape`
with the greatest `Area`. Return `null` if the canvas is empty.

The signature is `public Shape? FindLargest()` — note the `?`. That's
the nullable-reference annotation you saw in the lesson. The tests
assert with `Assert.Same`, which means you must return the **same
reference** that was added, not a new or copied shape.

This is the classic "track the best seen so far" loop pattern, but with
polymorphism under the hood — `s.Area` resolves to the right subclass
override at runtime, whether `s` is a `Circle`, a `Rectangle`, or your
new `Triangle` from Exercise 3.

**Tests:** `CanvasTests.*` (four of them)

**Reference**

- [Nullable reference types (Microsoft Learn)](https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references)
- [`Assert.Same` (xUnit)](https://xunit.net/docs/comparisons) — compares
  references, not values

---

# Next — the Bank capstone

Once every warm-up test is green, head over to the Bank project. It's
the capstone exercise for this lesson and the next couple of hours of
work — a small object-oriented bank simulator with accounts,
transactions, deposits, and withdrawals.

→ [**`projects/bank/README.md`**](../bank/README.md)

**How to prioritise the Bank extensions**

The Bank has 9 extensions ordered easiest → hardest. The **core spec**
exercises roughly 60% of the lesson material (classes, encapsulation,
constructors with validation, consuming `List<T>`). To hit the other 40%
(overloading, inheritance, polymorphism, writing generics), pick at
least one extension in this order of priority:

1. **§4 — Statement by date range** (20–30 min)
   Smallest entry into the advanced material. Drills **method
   overloading** + `DateTime` comparison.

2. **§7 — Generic ledger** (30–45 min)
   Write your own generic class, not just consume `List<T>`. If you've
   only ever read `List<T>`, this is the best next step.

3. **§8 — Account type hierarchy** (60–90 min) — **strongly recommended**
   `SavingsAccount` and `CurrentAccount` as subclasses of `Account`, with
   `virtual` / `override` for interest and withdrawal rules. The best
   single extension for drilling inheritance + polymorphism.

4. **§9 — Exporters** (2–3 hrs) — the capstone of the capstone
   Abstract `Exporter` base class, three concrete subclasses (CSV,
   Markdown, Excel), real NuGet package research, file I/O. This one
   throws up real-world messiness (binary formats don't fit a
   `string Export()` contract) and teaches you to refactor a base class
   when a subclass won't fit — one of the real lessons of working with
   inheritance.

Minimum target for the lesson: **core spec + §4 + (§8 or §9)**. Anything
beyond that is gravy.
