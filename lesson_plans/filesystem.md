# 45-minute Lesson — Composite pattern + Interfaces, via a file-system tree

Running domain: a file-system tree — `FSNode` (abstract base), `FileNode`,
`DirectoryNode`. Every method on the tree (`Size`, `FileCount`, `Print`) is
*recursive* through a mix of concrete types held in a single `List<FSNode>`.

This lesson builds **on top of** `oop.md` — assume students have seen
`abstract` / `virtual` / `override` with the `Shape` hierarchy. We're now
using those same tools for a **tree** instead of a flat list, which is where
polymorphism really earns its keep, and the natural bridge into interfaces.

Aim by end of the 45 minutes:

- They can read and write **polymorphic recursion**: a method on the base
  type that calls itself on children whose concrete type is unknown.
- They understand the **Composite pattern** — heterogeneous types sharing a
  contract, one of them holding a list of the base type.
- They can articulate the **abstract class vs interface** decision and pick
  correctly in a new scenario.
- They've seen **capability interfaces** (`ISearchable`, `ICompressible`)
  grant behaviour across unrelated hierarchies — the thing abstract classes
  cannot do.

---

## Time budget

| Block | Mins | What happens |
|---|---|---|
| A | 5  | Motivation — the naive "one class with maybe-children" attempt |
| B | 15 | Build the composite: abstract base + `FileNode` + `DirectoryNode` |
| C | 10 | Polymorphic recursion — `Size`, `FileCount`, `Print` |
| D | 5  | Encapsulation — why `children` is private (the cycle gotcha) |
| E | 10 | Interface angle — capability mixing across the hierarchy |

---

## Block A — Why not one class? (5 min)

Start by proposing the obvious design and shooting it down. Write this
*deliberately bad* sketch on screen:

```csharp
public class FSNode
{
    public string Name;
    public long? SizeBytes;              // set for files, null for directories
    public List<FSNode>? Children;       // set for directories, null for files

    public long Size()
    {
        if (Children == null) return SizeBytes!.Value;
        long total = 0;
        foreach (var c in Children) total += c.Size();
        return total;
    }

    public void Add(FSNode child) { Children!.Add(child); }
}
```

**Ask**: *"What's wrong with this?"* Pull three answers:

1. **Every method branches on `if (Children == null)`.** Two concepts —
   "I am a leaf" and "I am a container" — wedged into one class. The "if"
   is a smell; polymorphism is what replaces it.
2. **`Add()` exists on files.** A `FileNode.Add(child)` call should be a
   *compile error*, not a runtime `NullReferenceException`.
3. **Nullable fields everywhere.** `SizeBytes!.Value` and `Children!.Add`
   are both advertising "I don't really believe my own type".

**Say it out loud:** *"When you notice a class asking itself 'what kind of
thing am I?' at the start of every method, that's the shape of a missing
subclass."*

---

## Block B — Composite pattern: abstract base + two subtypes (15 min)

Rewrite it properly. Build incrementally — base first, then `FileNode`, then
`DirectoryNode`.

### B1. The abstract base

```csharp
public abstract class FSNode
{
    public string Name { get; }

    protected FSNode(string name)
    {
        Name = name;
    }

    public abstract long Size();
    public abstract int FileCount();
    public abstract void Print(int indent = 0);
}
```

**Points to hit:**

- `abstract class` — you can't `new FSNode()`. It only exists to be
  subclassed. (Callback to `Shape` in `oop.md`.)
- `protected` constructor — only subclasses can call it. A public
  constructor on an abstract class would be nonsense.
- Three `abstract` methods — no body, **must** be overridden.
- `Name` is the one thing *every* node has, so it lives on the base.

### B2. `FileNode` — the leaf

```csharp
public class FileNode : FSNode
{
    private readonly long sizeBytes;

    public FileNode(string name, long sizeBytes) : base(name)
    {
        if (sizeBytes < 0)
            throw new ArgumentException("Size cannot be negative.", nameof(sizeBytes));
        this.sizeBytes = sizeBytes;
    }

    public override long Size()
    {
        return sizeBytes;
    }

    public override int FileCount()
    {
        return 1;
    }

    public override void Print(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent * 2) + "- " + Name + " (" + sizeBytes + "b)");
    }
}
```

**Points to hit:**

- `: FSNode` — `FileNode` **IS-A** `FSNode`.
- `: base(name)` — pass the name up to the base-class constructor.
- `readonly` field — once set, can't change. A file's size is part of its
  identity here; this reinforces immutability.
- `FileCount()` returning `1` isn't a hack — it's *correct*. A file is
  one file. That one line is the base case of the recursion, and
  `DirectoryNode` relies on it being correct.

### B3. `DirectoryNode` — the composite

```csharp
public class DirectoryNode : FSNode
{
    private readonly List<FSNode> children = new();

    public DirectoryNode(string name) : base(name) { }

    public void Add(FSNode child)
    {
        children.Add(child);
    }

    public override long Size()
    {
        long total = 0;
        foreach (var child in children) total += child.Size();
        return total;
    }

    public override int FileCount()
    {
        int total = 0;
        foreach (var child in children) total += child.FileCount();
        return total;
    }

    public override void Print(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent * 2) + "+ " + Name + "/");
        foreach (var child in children) child.Print(indent + 1);
    }
}
```

**Points to hit:**

- `List<FSNode>` — children are *any* FSNode. Could be files, could be more
  directories, could be a type we haven't invented yet.
- `Add` only exists here. A `FileNode` has no `Add` — that's what we wanted.
- Each override delegates to children and combines the results. The combine
  step changes per method (sum, sum, side-effect) but the *shape* is the
  same.

---

## Block C — Polymorphic recursion in action (10 min)

Build a small tree live and call each method. This is the payoff moment.

```csharp
DirectoryNode root = new DirectoryNode("root");
DirectoryNode src = new DirectoryNode("src");
DirectoryNode tests = new DirectoryNode("tests");

root.Add(src);
root.Add(tests);
root.Add(new FileNode("README.md", 1200));

src.Add(new FileNode("Program.cs", 800));
src.Add(new FileNode("Helpers.cs", 450));

tests.Add(new FileNode("ProgramTests.cs", 1100));

root.Print();
Console.WriteLine();
Console.WriteLine($"Total size:  {root.Size()} bytes");
Console.WriteLine($"File count:  {root.FileCount()} files");
```

Expected output:

```
+ root/
  + src/
    - Program.cs (800b)
    - Helpers.cs (450b)
  + tests/
    - ProgramTests.cs (1100b)
  - README.md (1200b)

Total size:  3550 bytes
File count:  4 files
```

**Now point at the magic line** — inside `DirectoryNode.Size()`:

```csharp
foreach (var child in children) total += child.Size();
```

**Say:** *"`child` is typed as `FSNode`. At runtime it's sometimes a
`FileNode`, sometimes a `DirectoryNode`. `DirectoryNode` does **not** know
or care which it got. Calling `child.Size()` dispatches to the correct
override automatically — that's polymorphism. And because `DirectoryNode`
stores `FSNode`s and calls `FSNode` methods on them, a `DirectoryNode` can
recurse through `DirectoryNode`s without even knowing it's recursing."*

**Python/JS analogue:** duck-typing achieves the same dispatch, but without
the compile-time guarantee that every `FSNode` actually has a `Size`
method. Here, the compiler won't let you add an `FSNode` subclass that
forgot to implement `Size`.

> **The Composite pattern, in one sentence**: *"A base class defines a
> contract; one subclass is a leaf that does the work directly; another
> subclass is a container that holds a list of the base type and delegates
> the work to its children."* Students will see this shape again in ASTs,
> UI component trees, and menu hierarchies.

---

## Block D — Encapsulation: the cycle gotcha (5 min)

Walk back to `DirectoryNode` and ask: *"What if `children` were `public`?"*

```csharp
public List<FSNode> children = new();   // public — for the demo
```

Then live-code the disaster:

```csharp
DirectoryNode a = new DirectoryNode("a");
a.children.Add(a);                       // oops — a contains itself

a.Size();                                // StackOverflowException
```

**Say:** *"`Size()` recurses into children. One of the children is `a`
itself. `a.Size()` calls `a.Size()` calls `a.Size()`… stack overflow, the
process dies."*

The `Add` method doesn't prevent this by itself — but having `children`
private means the *only* way to add is through `Add`, which is *one place*
you can defend:

```csharp
public void Add(FSNode child)
{
    if (child == this)
        throw new ArgumentException("A directory cannot contain itself.");
    children.Add(child);
}
```

**Points to hit:**

- Encapsulation isn't about hiding for hiding's sake. It's about having a
  *single chokepoint* where you can enforce invariants.
- A public list has no chokepoint — every caller is on their honour.
- Callback to `oop.md` Example 1 (Vector2D swap-to-polar): same lesson,
  different shape. Private state buys you the freedom to enforce rules
  *and* to change implementation later.

> **If they ask**: *"Can you detect **deeper** cycles — a → b → a?"* Yes,
> but it needs a traversal with a `HashSet<FSNode>` of visited nodes. Good
> stretch exercise; don't derail.

---

## Block E — Interfaces: capability mixing (10 min)

**Segue:** *"Everything so far has been abstract-class shaped: a family of
types that share identity and state. Sometimes you want to grant a
*capability* — searching, compressing, encryption — to types that don't
share a family. That's what interfaces are for."*

### E1. A first interface

```csharp
public interface ISearchable
{
    FSNode? FindByName(string name);
}
```

- No constructor. No fields. No implementation.
- Pure contract: *"whoever implements me has a `FindByName`."*

Add it to both concrete types:

```csharp
public class FileNode : FSNode, ISearchable
{
    // ... existing code ...

    public FSNode? FindByName(string name)
    {
        return Name == name ? this : null;
    }
}

public class DirectoryNode : FSNode, ISearchable
{
    // ... existing code ...

    public FSNode? FindByName(string name)
    {
        if (Name == name) return this;
        foreach (var child in children)
        {
            if (child is ISearchable s)
            {
                var hit = s.FindByName(name);
                if (hit != null) return hit;
            }
        }
        return null;
    }
}
```

**Points to hit:**

- `FileNode : FSNode, ISearchable` — one base class, many interfaces.
  That's the big structural difference from abstract classes: C# allows
  only one base class, but any number of interfaces.
- `child is ISearchable s` — pattern-matching type check. If `child`
  implements `ISearchable`, bind it to `s`. Otherwise skip.

### E2. The payoff — a capability that escapes the hierarchy

Introduce a completely unrelated type that *also* wants to be searchable:

```csharp
public class Bookmark : ISearchable     // NOT an FSNode at all
{
    public string Label { get; }
    public string Url { get; }

    public Bookmark(string label, string url)
    {
        Label = label;
        Url = url;
    }

    public FSNode? FindByName(string name)
    {
        return null;   // bookmarks don't match FSNode searches, but they participate in the contract
    }
}
```

Or — even better — a capability that truly crosses the hierarchy:

```csharp
public interface ICompressible
{
    long CompressedSize();
}

public class FileNode : FSNode, ISearchable, ICompressible
{
    // ...
    public long CompressedSize()
    {
        return sizeBytes / 2;   // pretend gzip
    }
}

// DirectoryNode is NOT ICompressible — maybe we only support compressing
// files, not folders. That's fine: DirectoryNode just doesn't list the interface.
```

**Say:** *"An interface lets a class opt into a capability without buying
the whole family. `FileNode` IS-A `FSNode` AND can do the compressible
thing. `DirectoryNode` IS-A `FSNode` but skips compression. An abstract
class can't split like that — if `FSNode` had a `CompressedSize` method,
every subclass would have to provide one."*

### E3. The decision rule

Write this on the whiteboard and leave it:

> - **Abstract class** when the subclasses share state and identity
>   (*"a SavingsAccount **is a kind of** Account"*).
>   Use it for the backbone of a type family.
>
> - **Interface** when you're describing a capability
>   (*"this thing **can be** searched / compressed / notified"*),
>   especially if unrelated types might want it.
>
> The two are not competitors. Real designs use both — a base class for
> the family, interfaces for the capabilities.

---

## Exercise bank

Hand students a subset of these, ranked by difficulty. Any of them works
as a standalone exercise; the last one is a mini-project.

1. **`LargestFile()`** on `FSNode` — recurse, return the `FileNode?` with
   the greatest `Size()`.
2. **`FilterByExtension(string ext)`** — return `List<FileNode>` of all
   files whose `Name` ends with `ext`.
3. **`CountByExtension()`** — return `Dictionary<string, int>` mapping
   extension to file count.
4. **`Depth()`** — how many levels deep is the deepest file?
5. **Cycle-safe `Add`** — detect a deeper cycle (a→b→a) using a visited
   set, not just `child == this`.
6. **Pretty `Print`** — redo `Print` with tree-drawing characters
   (`├──`, `└──`, `│  `) so it renders like `tree` on Unix. Surprisingly
   tricky — students need to know whether each child is the *last* sibling
   to pick the right glyph. Genuine boss fight.
7. **Symlinks** — add a `SymlinkNode : FSNode` that holds a reference to
   another `FSNode`. Decide: does `Size()` follow the link or return 0?
   (Real answer: it depends what you're measuring. Either is defensible —
   the exercise is *making the decision and documenting it*.)

---

## Appendix — if you have spare time

- **Default interface methods.** C# 8+ lets an interface provide a default
  implementation. Show one:
  ```csharp
  public interface ISearchable
  {
      FSNode? FindByName(string name);
      bool Contains(string name) => FindByName(name) != null;   // default
  }
  ```
  Useful for extending an interface without breaking existing implementers.
  Controversial — don't oversell.
- **`sealed` on a composite leaf.** `sealed class FileNode : FSNode` says
  *"nothing inherits from me"*. For leaves it's often the right call.
- **`readonly` collection exposure.** Instead of hiding `children`
  entirely, expose it as `IReadOnlyList<FSNode>` — callers can iterate,
  but can't mutate without going through `Add`.
