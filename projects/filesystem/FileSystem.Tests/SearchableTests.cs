using FileSystemApp;

namespace FileSystemApp.Tests;

public class SearchableTests
{
    // Shared fixture:  root / { src / { Program.cs, Helpers.cs }, README.md }
    private static DirectoryNode BuildTree(
        out FileNode readme, out FileNode program, out FileNode helpers, out DirectoryNode src)
    {
        DirectoryNode root = new DirectoryNode("root");
        src = new DirectoryNode("src");
        readme = new FileNode("README.md", 100);
        program = new FileNode("Program.cs", 200);
        helpers = new FileNode("Helpers.cs", 300);

        root.Add(src);
        root.Add(readme);
        src.Add(program);
        src.Add(helpers);

        return root;
    }

    [Fact]
    public void FindByName_FindsTopLevelFile()
    {
        DirectoryNode root = BuildTree(out FileNode readme, out _, out _, out _);
        Assert.Same(readme, root.FindByName("README.md"));
    }

    [Fact]
    public void FindByName_FindsNestedFile()
    {
        DirectoryNode root = BuildTree(out _, out FileNode program, out _, out _);
        Assert.Same(program, root.FindByName("Program.cs"));
    }

    [Fact]
    public void FindByName_FindsDirectoryByName()
    {
        DirectoryNode root = BuildTree(out _, out _, out _, out DirectoryNode src);
        Assert.Same(src, root.FindByName("src"));
    }

    [Fact]
    public void FindByName_ReturnsNullWhenNotFound()
    {
        DirectoryNode root = BuildTree(out _, out _, out _, out _);
        Assert.Null(root.FindByName("missing.txt"));
    }

    [Fact]
    public void FindByName_MatchesRootItself()
    {
        DirectoryNode root = BuildTree(out _, out _, out _, out _);
        Assert.Same(root, root.FindByName("root"));
    }

    [Fact]
    public void FindByName_ReturnsFirstDepthFirstMatch()
    {
        // Two files with the same name in different branches.
        // Depth-first means the one in the first branch visited wins.
        DirectoryNode root = new DirectoryNode("root");
        DirectoryNode first = new DirectoryNode("first");
        DirectoryNode second = new DirectoryNode("second");
        FileNode firstHit = new FileNode("same.txt", 1);
        FileNode secondHit = new FileNode("same.txt", 999);
        root.Add(first);
        root.Add(second);
        first.Add(firstHit);
        second.Add(secondHit);

        Assert.Same(firstHit, root.FindByName("same.txt"));
    }

    // An FSNode reference reaches FindByName via the ISearchable
    // interface — this test exists to lock that in.
    [Fact]
    public void FindByName_WorksThroughISearchableReference()
    {
        DirectoryNode root = BuildTree(out FileNode readme, out _, out _, out _);
        ISearchable s = root;
        Assert.Same(readme, s.FindByName("README.md"));
    }
}
