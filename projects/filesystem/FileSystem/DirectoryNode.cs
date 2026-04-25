namespace FileSystemApp;

// A composite node. A directory has a name and a list of children,
// each of which is itself an FSNode — so a directory can contain
// files AND other directories.
//
// Notice how Size(), FileCount(), and Print() all have the same
// shape: "do my own bit, then delegate to every child". That
// delegation is what makes the tree recursive — and it works because
// `children` is declared as `List<FSNode>`, not `List<FileNode>` or
// `List<DirectoryNode>`. The directory doesn't know (or care) which
// concrete type each child is; it just calls the method on the base
// class and the correct override runs at runtime. That's polymorphism.
//
// The `children` list is private, and the ONLY way to put something in
// it is through Add(). That single chokepoint is where we enforce the
// rules — e.g. "a directory must not contain itself" — so the rest of
// the class can trust its state.
public class DirectoryNode : FSNode, ISearchable
{
    private readonly List<FSNode> children = new();

    public DirectoryNode(string name) : base(name)
    {
    }

    // Expose children as read-only. Callers can enumerate them, but
    // cannot Add / Remove / Clear — the only mutation path is Add().
    public IReadOnlyList<FSNode> Children => children;

    public void Add(FSNode child)
    {
        if (child == null)
            throw new ArgumentNullException(nameof(child));
        if (child == this)
            throw new ArgumentException("A directory cannot contain itself.", nameof(child));
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

    // ISearchable — depth-first, left-to-right search.
    //   1. Does THIS directory match? If so, return it.
    //   2. Otherwise ask each child in turn (if the child is searchable).
    //   3. If nothing matches, return null.
    //
    // The `child is ISearchable s` pattern-match future-proofs this
    // method — it's fine if someone later adds an FSNode subclass that
    // deliberately opts out of searching.
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

    public override FileNode? LargestFile()
    {
        FileNode? largest = null;
        foreach (FSNode child in children)
        {
            if (child is FileNode file && (largest == null || file.Size() > largest.Size()))
            {
                largest = file;
            }
            else if (child is DirectoryNode directory)
            {
                largest = directory.LargestFile();
            }
        }

        return largest;
    }

    public override List<FileNode> FilterByExtension(string ext)
    {
        List<FileNode> result = [];
        foreach (FSNode child in children)
        {
            result.AddRange(child.FilterByExtension(ext));
        }

        return result;
    }
}
