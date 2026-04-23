# File System — OOP practice project

A small object-oriented model of a file-system tree. Unlike the Bank capstone
(which you build from stubs), this project ships as **working, end-state code**
— your job is to **read it, run it, understand *why* it is shaped the way it
is**, and then **extend it** through the exercises at the bottom of this README.

The project demonstrates three ideas together:

- **The Composite pattern** — one recursive tree, built from heterogeneous
  node types, all sharing a common base contract.
- **Polymorphism through overriding** — `Size()`, `FileCount()`, and `Print()`
  behave differently on a `FileNode` vs a `DirectoryNode`, and the recursion
  inside `DirectoryNode` works without ever knowing which concrete type its
  children are.
- **Interfaces as capabilities** — `ISearchable` declares the "find me by
  name" capability, implemented by both node types, and designed so that a
  future non-`FSNode` type could opt in too.

---

## How this project is organised

```
projects/filesystem/
├── FileSystem.slnx               ← solution file (open this in Visual Studio)
├── global.json                   ← pins .NET 10
├── FileSystem/
│   ├── FileSystem.csproj         ← console app
│   ├── Program.cs                ← demo — run to see the tree in action
│   ├── FSNode.cs                 ← abstract base class
│   ├── FileNode.cs               ← leaf: file
│   ├── DirectoryNode.cs          ← composite: directory
│   └── ISearchable.cs            ← capability interface
└── FileSystem.Tests/
    ├── FileSystem.Tests.csproj   ← xUnit tests (all passing out of the box)
    ├── FileNodeTests.cs
    ├── DirectoryNodeTests.cs
    └── SearchableTests.cs
```

The `FileSystem.csproj` root namespace is **`FileSystemApp`** so there's no
clash between the project folder name and .NET's own `System.IO` concepts.
Every source file starts with `namespace FileSystemApp;`.

---

## Running it

From `projects/filesystem/`:

```sh
dotnet test                       # runs every test — all 31 should pass
dotnet run --project FileSystem   # runs Program.cs (the demo)
```

Or open `FileSystem.slnx` in Visual Studio and use Test Explorer / F5.

Expected demo output:

```
=== TREE ===
+ root/
  + src/
    - Program.cs (800b)
    ...
  + tests/
    ...
  - README.md (1200b)

Total size : 10870 bytes
File count : 12 files
...
```

---

## Guided tour of the code

Read the source files in this order — each one builds on the previous:

### 1. `FSNode.cs` — the abstract base

The contract every node must satisfy. It carries the one piece of shared
state (`Name`) and declares three **abstract methods** that every subclass
must implement:

```csharp
public abstract long Size();
public abstract int  FileCount();
public abstract void Print(int indent = 0);
```

`abstract class FSNode` means *"you can never instantiate FSNode directly"*.
It only exists to be subclassed. The `protected` constructor makes that
explicit — only subclasses can call it.

### 2. `FileNode.cs` — the leaf

A file has a name and a size. No children. The three abstract methods are
implemented directly: `Size()` returns its own bytes, `FileCount()` returns
`1` (the base case of the recursion), `Print()` writes a single line.

Notice there is **no `Add` method**. A `FileNode` literally cannot have
children — that's a compile-time guarantee, not a runtime check.

### 3. `DirectoryNode.cs` — the composite

A directory owns a `List<FSNode>` of children. The magic lives here:

```csharp
public override long Size()
{
    long total = 0;
    foreach (var child in children) total += child.Size();
    return total;
}
```

The directory **does not know** whether each `child` is a `FileNode` or
another `DirectoryNode`. It calls `child.Size()` and trusts that the right
override runs. That's polymorphism — and it's only possible because `Size()`
is declared on the base class.

Three other details worth noticing in this file:

- `children` is **private** and exposed through `IReadOnlyList<FSNode>
  Children`. The only way to mutate the list is `Add()`, which is where we
  enforce rules like *"a directory cannot contain itself"*.
- `Add()` is the **chokepoint for invariants**. If we ever need a new rule
  (e.g. no duplicate names), there's one place to add it.
- `FindByName()` uses `child is ISearchable s` pattern matching rather than
  a hard cast — this future-proofs it against a future subclass that opts
  out of searching.

### 4. `ISearchable.cs` — the capability interface

```csharp
public interface ISearchable
{
    FSNode? FindByName(string name);
}
```

Both `FileNode` and `DirectoryNode` implement this. A file matches only
itself; a directory matches itself or recurses into its children.

**Why an interface instead of another abstract method on `FSNode`?** Because
"being searchable" is a *capability*, not part of what it means to be a
node. In a larger codebase you might want a `Bookmark` or a `RemoteUrl` to
be searchable too — neither of which is a file-system node. An interface
lets any class opt in to the capability without buying into the hierarchy.

---

## Core concepts, in one paragraph each

### Abstract class vs interface

Use an **abstract class** when subclasses genuinely share state and identity
(*"a `FileNode` **is a kind of** `FSNode`"*). The base class can carry
fields, a constructor, and default implementations — everything a subclass
inherits.

Use an **interface** when you're describing a capability (*"this thing **can
be** searched"*), especially if unrelated types might want it. An interface
is a pure contract with no fields or constructor; a class can implement
many interfaces but extend only one base class.

This project uses **both on purpose**: `FSNode` is the backbone of the type
family (abstract class); `ISearchable` is an optional capability layered on
top (interface). When you're deciding how to model a new feature, that's
the question to ask: *is it part of what this type fundamentally IS, or is
it a capability it happens to offer?*

### The Composite pattern

A recursive tree of heterogeneous types, where:

- A **base type** declares operations that work on a subtree (`Size`,
  `FileCount`, `Print`).
- A **leaf** subclass answers those operations directly (`FileNode`).
- A **composite** subclass holds a list of the *base* type and delegates
  the operations to its children (`DirectoryNode`).

The payoff: client code (`Program.cs`, the tests) can treat every node
uniformly — no "is this a file or a folder?" checks. The tree does the
right thing automatically.

You will see this same shape in ASTs, UI component trees, and menu
hierarchies. Once you've written it once, you recognise it everywhere.

### Polymorphism through `abstract` / `override`

- `abstract` on the base → "no default, every subclass MUST implement this".
- `override` on the subclass → "here is my version".

At runtime, calling `someFSNode.Size()` dispatches to the override matching
the **actual** concrete type of the object, not the type of the variable.
That's what makes the `foreach (var child in children) total += child.Size()`
line so compact: it works identically whether `child` is a file or a
directory.

---

## Exercises

All 31 tests in the project pass already — they document the **existing
behaviour**. The exercises below ask you to **add new behaviour** on top.
For each exercise, write the code in the relevant `.cs` file **and** add
tests for it in `FileSystem.Tests/` (create a new test file or extend an
existing one).

Do them in order — they roughly escalate in difficulty.

### Exercise 1 — `LargestFile()`

Add a method on `FSNode`:

```csharp
public abstract FileNode? LargestFile();
```

Behaviour:

- On a `FileNode`: returns itself.
- On a `DirectoryNode`: returns the `FileNode` in the subtree with the
  greatest `Size()`. Returns `null` if the directory (and all descendants)
  contain no files at all.
- Ties can be broken either way — pick one and document it.

Add at least three tests: one file, a flat directory of files, and a
nested directory.

### Exercise 2 — `FilterByExtension(string ext)`

Add a method on `FSNode`:

```csharp
public abstract List<FileNode> FilterByExtension(string ext);
```

Behaviour:

- Returns every `FileNode` in the subtree whose `Name` ends with `ext`.
- Case-insensitive: `"README.MD"` should match `ext = ".md"`.
- On a `FileNode`: returns a list containing itself if it matches,
  otherwise an empty list.
- On a `DirectoryNode`: flatten the results from every child into one list.

Hints:

- `string.EndsWith(string value, StringComparison comparisonType)` takes a
  comparison mode — `StringComparison.OrdinalIgnoreCase` is what you want.
- You can build the result list with a `foreach` and `AddRange`.

### Exercise 3 — `CountByExtension()`

Add a method on `FSNode`:

```csharp
public abstract Dictionary<string, int> CountByExtension();
```

Behaviour:

- Returns a map from extension (e.g. `".cs"`, `".md"`) to the number of
  files in the subtree with that extension. Lowercase the extension before
  storing.
- Files with no dot in their name (e.g. `"Makefile"`) should use the empty
  string `""` as their key.
- On a `FileNode`: returns a map with exactly one entry.
- On a `DirectoryNode`: merge the child maps, summing counts for shared
  keys.

Hints:

- `Path.GetExtension(name)` returns `".cs"` for `"Program.cs"`, and `""`
  for `"Makefile"`. Lowercase it with `.ToLowerInvariant()`.
- Merging two `Dictionary<string, int>`s: iterate the right-hand entries
  and use `TryGetValue` on the left-hand one.

### Exercise 4 — `Depth()`

Add a method on `FSNode`:

```csharp
public abstract int Depth();
```

Behaviour:

- On a `FileNode`: returns `0`.
- On a `DirectoryNode`:
  - `0` if it has no children.
  - Otherwise, `1 + max(child.Depth() for each child)`.

So a directory containing only one file is depth `1`; a directory
containing a directory containing a file is depth `2`; and so on.

### Exercise 5 — Pretty printing

Add a new method on `FSNode`:

```csharp
public abstract void PrettyPrint(string prefix = "", bool isLast = true);
```

Render the tree with proper tree-drawing characters, like the Unix `tree`
command:

```
root/
├── src/
│   ├── Program.cs
│   ├── FSNode.cs
│   └── DirectoryNode.cs
├── tests/
│   └── FileNodeTests.cs
└── README.md
```

The characters you need are:

- `├── ` — a sibling that has more siblings after it
- `└── ` — the last sibling
- `│   ` — a vertical bar for continuing an ancestor's branch
- `    ` — four spaces where the ancestor had no more siblings

This one is surprisingly tricky — the directory needs to know, for each
child, whether it's the **last** child, because that decides `├──` vs
`└──`. And when recursing into a child, the prefix you pass down depends
on whether *you* were the last sibling (pass `"│   "`) or not (pass
`"    "`).

**Strong hint**: iterate with an index so you can check
`index == children.Count - 1`.

### Exercise 6 — Cycle-safe containment

The current `Add()` only rejects `child == this` (the shallowest cycle).
It does NOT catch a deeper cycle like `a.Add(b); b.Add(a);`, which would
cause `Size()` to recurse forever and blow the stack.

Add cycle detection that catches any cycle, at any depth. Two common ways:

- **Eager check at Add time**: walk the entire subtree of `child` and
  reject if `this` appears anywhere in it.
- **Lazy check at traversal time**: keep a `HashSet<FSNode>` of visited
  nodes during `Size()` / `FileCount()` / `Print()` and skip anything
  you've already seen.

Pick one, implement it, write a test that *used* to stack-overflow and
now throws a clear exception (for the eager approach) or returns a
sensible answer (for the lazy approach).

### Exercise 7 — `SymlinkNode`

Introduce a third concrete type:

```csharp
public class SymlinkNode : FSNode, ISearchable
{
    private readonly FSNode target;
    ...
}
```

A symlink has a name and a `target` — another `FSNode` somewhere else in
the tree. You'll need to decide how `Size()`, `FileCount()`, and `Print()`
behave on a symlink. **There is no single right answer** — the real `tree`
command has a flag to control it. Pick a behaviour and justify it:

- Option A — **follow the link**: delegate every method to `target`.
  Matches `du -L`, at the cost of over-counting if a file is pointed to
  from multiple places.
- Option B — **don't follow**: `Size()` returns `0`, `FileCount()` returns
  `0`, `Print()` renders `"-> target.Name"`. Matches `du` without `-L`.
- Option C — **follow for `Size` but not `FileCount`**, or any other
  combination you can defend.

Document your choice in the class's doc comment. Write tests that pin it
down.

**Watch out:** a symlink whose target includes the symlink itself creates
a cycle. Make sure your Exercise 6 cycle detection still works.

### Exercise 8 — A second interface: `ICompressible`

Add a second capability interface:

```csharp
public interface ICompressible
{
    long CompressedSize();
}
```

Implement it on `FileNode` only — pretend that files can be compressed
(e.g. `CompressedSize()` returns `sizeBytes / 2`) but directories cannot
be compressed as a unit.

Then add a method on `DirectoryNode`:

```csharp
public long TotalCompressedSize();
```

that sums the `CompressedSize()` of every `ICompressible` file in its
subtree. Use `child is ICompressible c` pattern matching — don't force
every `FSNode` to be `ICompressible`.

**This exercise is the point of having interfaces at all.** `FileNode` now
implements both `ISearchable` AND `ICompressible`; `DirectoryNode`
implements only `ISearchable`. An abstract class on `FSNode` with
`CompressedSize()` would have forced every subclass — including
directories — to answer, which is wrong.

### Exercise 9 (stretch) — Generalise with LINQ

If you've met LINQ already, rewrite `FilterByExtension`, `CountByExtension`,
and `LargestFile` using `Where`, `Sum`, `GroupBy`, `ToDictionary`, and
`MaxBy`. You'll need a helper that enumerates every descendant as an
`IEnumerable<FSNode>` (or `IEnumerable<FileNode>`) so LINQ has something to
chain off.

Compare the two versions. Which one reads better? Which one would you want
to debug at 2am?

---

## Further reading

- [C# `abstract` modifier](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract)
- [C# interfaces](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces)
- [Polymorphism in C#](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism)
- [Pattern matching (`is` patterns)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns)
- [Composite pattern (Refactoring Guru)](https://refactoring.guru/design-patterns/composite) — the design pattern this whole project is an instance of.
