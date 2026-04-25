namespace FileSystemApp;

// The abstract base for every node in the tree. An FSNode has a name,
// knows how to report its total size and how many files it contains,
// and knows how to print itself (with indentation).
//
// We make FSNode `abstract` because you never need a plain "FSNode" —
// every concrete node is either a file (a leaf) or a directory (a
// composite). Forcing subclasses to provide Size / FileCount / Print is
// the whole point: each concrete type decides how to answer those three
// questions, and the rest of the code just trusts that it can ask.
//
// This is the Composite design pattern in its classic shape:
//   • a shared contract on the base (this class)
//   • a "leaf" subclass that answers directly (FileNode)
//   • a "composite" subclass that holds children of the base type and
//     delegates the answer to them (DirectoryNode)
public abstract class FSNode
{
    public string Name { get; }

    protected FSNode(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be blank.", nameof(name));
        Name = name;
    }

    // Total size in bytes held at or below this node.
    //   FileNode      → its own size
    //   DirectoryNode → the sum of its children's sizes (recursive)
    public abstract long Size();

    // Number of files at or below this node.
    //   FileNode      → 1
    //   DirectoryNode → the sum of its children's FileCount (recursive)
    public abstract int FileCount();

    // Write a human-readable representation of this node to the console.
    // `indent` controls nesting — each level adds two spaces of leading
    // whitespace, so DirectoryNode can call child.Print(indent + 1).
    public abstract void Print(int indent = 0);

    public abstract FileNode? LargestFile();
}
