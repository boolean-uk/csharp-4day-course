namespace FileSystemApp;

// A capability: "I know how to find a node by name".
//
// Both FileNode and DirectoryNode implement this — a file matches only
// itself; a directory matches itself or, failing that, recurses into
// its children. Because this is an INTERFACE rather than a method on
// the FSNode base class, anything else you ever add to the tree can
// opt into searching just by implementing FindByName — it doesn't have
// to live inside the FSNode hierarchy at all.
//
// That cross-hierarchy reach is the real reason to pick an interface
// over an abstract-class method. See README.md § "Abstract class vs
// interface" for the decision rule.
public interface ISearchable
{
    // Return the first node whose Name matches, or null if no match.
    // "First" is a depth-first, left-to-right walk in the current
    // implementations — the root (or self) is checked before its
    // children are.
    FSNode? FindByName(string name);
}
