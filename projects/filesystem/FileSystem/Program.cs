using FileSystemApp;

// Demo scaffolding — builds a small tree and shows off the recursive
// methods. Press F5 in Visual Studio (or `dotnet run` from the project
// folder) to see it.
//
// The point to notice: the recursion lives inside DirectoryNode, but
// DirectoryNode never checks "is this child a file or a directory?".
// It just calls child.Size() / child.FileCount() / child.Print() and
// the correct override runs automatically — that's polymorphism.

DirectoryNode root = new DirectoryNode("root");
DirectoryNode src = new DirectoryNode("src");
DirectoryNode tests = new DirectoryNode("tests");
DirectoryNode docs = new DirectoryNode("docs");

root.Add(src);
root.Add(tests);
root.Add(docs);
root.Add(new FileNode("README.md", 1200));
root.Add(new FileNode(".gitignore", 120));

src.Add(new FileNode("Program.cs", 800));
src.Add(new FileNode("FSNode.cs", 650));
src.Add(new FileNode("FileNode.cs", 540));
src.Add(new FileNode("DirectoryNode.cs", 980));
src.Add(new FileNode("ISearchable.cs", 260));

tests.Add(new FileNode("FileNodeTests.cs", 1100));
tests.Add(new FileNode("DirectoryNodeTests.cs", 1700));
tests.Add(new FileNode("SearchableTests.cs", 900));

docs.Add(new FileNode("intro.md", 420));
docs.Add(new FileNode("reference.md", 2200));

Console.WriteLine("=== TREE ===");
root.Print();
Console.WriteLine();

Console.WriteLine($"Total size : {root.Size()} bytes");
Console.WriteLine($"File count : {root.FileCount()} files");
Console.WriteLine();

// ISearchable in action. Notice that FindByName is called on `root`
// (a DirectoryNode) but returns an FSNode — the concrete type of the
// result depends on what matched.
Console.WriteLine("=== SEARCH ===");
FSNode? readme = root.FindByName("README.md");
FSNode? missing = root.FindByName("does-not-exist.txt");
FSNode? testsDir = root.FindByName("tests");

Console.WriteLine($"README.md found?          {readme != null}  ({readme?.Size()} bytes)");
Console.WriteLine($"does-not-exist.txt found? {missing != null}");
Console.WriteLine($"'tests' directory found?  {testsDir != null}  ({testsDir?.FileCount()} files)");

// Try to contain the root inside itself — the chokepoint in Add()
// catches this at runtime. Caught here so the demo keeps running.
Console.WriteLine();
Console.WriteLine("=== SELF-CONTAINMENT GUARD ===");
try
{
    root.Add(root);
    Console.WriteLine("This line should never run.");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Blocked: {ex.Message}");
}

// TODO (students): extend the demo as you work through the README
// exercises. Ideas —
//   • Call your LargestFile() and print the winner.
//   • Call your FilterByExtension(".cs") and print each match.
//   • Build a much deeper tree and check Size / FileCount stay correct.
//   • After implementing the pretty-print exercise, swap the call to
//     root.Print() above for root.PrettyPrint() to see the difference.

Console.WriteLine("=== PRETTY PRINT ===");
root.PrettyPrint();

