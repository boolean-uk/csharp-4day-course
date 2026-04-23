namespace OopSandbox;

// ═══════════════════════════════════════════════════════════════════════
// FILESYSTEM LESSON — Block A: the naive "one class" starting point.
// ═══════════════════════════════════════════════════════════════════════
//
// This class is DELIBERATELY WRONG. See lesson_plans/filesystem.md § Block A.
// Pull this file up in class, pick it apart, then rewrite live as a proper
// Composite: an abstract FSNode base + FileNode (leaf) + DirectoryNode
// (composite). The end-state reference code lives at projects/filesystem/.
//
// Smells to call out before tearing it up:
//
//   1. Every method branches on `if (Children == null)`. That `if` is the
//      shape of a missing subclass — two concepts ("I'm a leaf" and "I'm a
//      container") wedged into one class.
//
//   2. `Add(child)` lives on every FSNode — including files. A
//      `fileNode.Add(...)` call should be a COMPILE ERROR, not a runtime
//      NullReferenceException. We have no compile-time distinction between
//      a leaf and a container.
//
//   3. Nullable fields everywhere. `SizeBytes!.Value` and `Children!.Add`
//      are both announcing "I don't really believe my own type".
//
// The refactor (Block B onward) replaces this class with:
//
//   abstract class FSNode                        // shared contract
//   class FileNode      : FSNode, ISearchable    // leaf
//   class DirectoryNode : FSNode, ISearchable    // composite
// ═══════════════════════════════════════════════════════════════════════

public class FSNode
{
    public string Name;
    public long? SizeBytes;              // set for files, null for directories
    public List<FSNode>? Children;       // set for directories, null for files

    public FSNode(string name)
    {
        Name = name;
    }

    public long Size()
    {
        if (Children == null) return SizeBytes!.Value;    // null-forgiving — smell
        long total = 0;
        foreach (var c in Children) total += c.Size();
        return total;
    }

    public int FileCount()
    {
        if (Children == null) return 1;                   // branch on "am I a leaf?"
        int total = 0;
        foreach (var c in Children) total += c.FileCount();
        return total;
    }

    public void Add(FSNode child)
    {
        Children!.Add(child);    // boom at runtime if `this` is a file
    }
}
