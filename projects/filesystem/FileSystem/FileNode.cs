namespace FileSystemApp;

// A leaf in the tree. A file has a name and a size in bytes, and that
// is all. It cannot have children, so there is no Add method — a
// compile error is exactly the right answer if some caller tries.
//
// FileNode implements two contracts:
//   • FSNode      — the base-class contract for every node
//   • ISearchable — the capability contract for "find me by name"
//
// Notice that FileCount() returns a hard-coded 1. That's not a hack,
// it's the base case of the recursion. DirectoryNode relies on every
// leaf correctly reporting "I am one file".
public class FileNode : FSNode, ISearchable
{
    private readonly long sizeBytes;

    public FileNode(string name, long sizeBytes) : base(name)
    {
        if (sizeBytes < 0)
            throw new ArgumentException("Size cannot be negative.", nameof(sizeBytes));
        this.sizeBytes = sizeBytes;
    }

    public long SizeBytes => sizeBytes;

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

    // ISearchable — a file matches only itself.
    public FSNode? FindByName(string name)
    {
        return Name == name ? this : null;
    }

    public override FileNode LargestFile()
    {
        return this;
    }

    public override List<FileNode> FilterByExtension(string ext)
    {
        return Name.EndsWith(ext, StringComparison.OrdinalIgnoreCase)
            ? [this]
            : new List<FileNode>();
    }

    public override Dictionary<string, int> CountByExtension()
    {
        return new Dictionary<string, int> { { Path.GetExtension(Name).ToLowerInvariant(), 1 } };
    }

    public override int Depth()
    {
        return 0;
    }
}
