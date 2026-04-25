using FileSystemApp;

namespace FileSystemApp.Tests;

public class DirectoryNodeTests
{
    [Fact]
    public void Constructor_AssignsName()
    {
        DirectoryNode d = new DirectoryNode("root");
        Assert.Equal("root", d.Name);
    }

    [Fact]
    public void Constructor_ThrowsOnBlankName()
    {
        Assert.Throws<ArgumentException>(() => new DirectoryNode(""));
    }

    [Fact]
    public void Empty_SizeIsZero()
    {
        DirectoryNode d = new DirectoryNode("empty");
        Assert.Equal(0, d.Size());
    }

    [Fact]
    public void Empty_FileCountIsZero()
    {
        DirectoryNode d = new DirectoryNode("empty");
        Assert.Equal(0, d.FileCount());
    }

    [Fact]
    public void Size_SumsDirectFileChildren()
    {
        DirectoryNode d = new DirectoryNode("root");
        d.Add(new FileNode("a.txt", 100));
        d.Add(new FileNode("b.txt", 250));
        Assert.Equal(350, d.Size());
    }

    [Fact]
    public void Size_RecursesIntoNestedDirectories()
    {
        DirectoryNode root = new DirectoryNode("root");
        DirectoryNode sub = new DirectoryNode("sub");
        root.Add(sub);
        root.Add(new FileNode("top.txt", 10));
        sub.Add(new FileNode("deep.txt", 40));

        Assert.Equal(50, root.Size());
    }

    [Fact]
    public void FileCount_SumsDirectFileChildren()
    {
        DirectoryNode d = new DirectoryNode("root");
        d.Add(new FileNode("a.txt", 10));
        d.Add(new FileNode("b.txt", 10));
        d.Add(new FileNode("c.txt", 10));
        Assert.Equal(3, d.FileCount());
    }

    [Fact]
    public void FileCount_RecursesAndCountsOnlyFiles()
    {
        DirectoryNode root = new DirectoryNode("root");
        DirectoryNode sub = new DirectoryNode("sub");
        DirectoryNode subsub = new DirectoryNode("subsub");
        root.Add(sub);
        sub.Add(subsub);

        root.Add(new FileNode("top.txt", 1));
        sub.Add(new FileNode("mid.txt", 1));
        subsub.Add(new FileNode("deep.txt", 1));

        Assert.Equal(3, root.FileCount());
    }

    [Fact]
    public void FileCount_EmptyDirectoriesDoNotCount()
    {
        DirectoryNode root = new DirectoryNode("root");
        root.Add(new DirectoryNode("emptyA"));
        root.Add(new DirectoryNode("emptyB"));
        Assert.Equal(0, root.FileCount());
    }

    [Fact]
    public void Add_RejectsSelfReference()
    {
        DirectoryNode d = new DirectoryNode("self");
        Assert.Throws<ArgumentException>(() => d.Add(d));
    }

    [Fact]
    public void Add_RejectsNull()
    {
        DirectoryNode d = new DirectoryNode("d");
        Assert.Throws<ArgumentNullException>(() => d.Add(null!));
    }

    [Fact]
    public void Children_IsReadOnly()
    {
        DirectoryNode d = new DirectoryNode("d");
        d.Add(new FileNode("x.txt", 1));
        Assert.IsAssignableFrom<IReadOnlyList<FSNode>>(d.Children);
        Assert.Single(d.Children);
    }

    [Fact]
    public void Children_ExposesAddedInOrder()
    {
        DirectoryNode d = new DirectoryNode("d");
        FileNode a = new FileNode("a.txt", 1);
        FileNode b = new FileNode("b.txt", 2);
        FileNode c = new FileNode("c.txt", 3);
        d.Add(a);
        d.Add(b);
        d.Add(c);

        Assert.Equal(new FSNode[] { a, b, c }, d.Children);
    }

    [Fact]
    public void LargestFile_ReturnsLargestFile()
    {
        DirectoryNode d = new DirectoryNode("d");
        FileNode a = new FileNode("a.txt", 1);
        FileNode b = new FileNode("b.txt", 2);
        FileNode c = new FileNode("c.txt", 3);
        d.Add(a);
        d.Add(b);
        d.Add(c);

        Assert.Equal(c, d.LargestFile());
    }

    [Fact]
    public void LargestFile_ReturnsLargestFileInNestedDirectory()
    {
        DirectoryNode d = new DirectoryNode("d");
        DirectoryNode d2 = new DirectoryNode("d2");
        d.Add(d2);
        FileNode a = new FileNode("a.txt", 1);
        FileNode b = new FileNode("b.txt", 2);
        FileNode c = new FileNode("c.txt", 3);
        d2.Add(a);
        d2.Add(b);
        d2.Add(c);

        Assert.Equal(c, d.LargestFile());
    }

    [Fact]
    public void FilterByExtension_ReturnsFilesWithMatchingExtension()
    {
        DirectoryNode d = new DirectoryNode("d");
        FileNode a = new FileNode("a.txt", 1);
        FileNode b = new FileNode("b.md", 2);
        FileNode c = new FileNode("c.txt", 3);
        d.Add(a);
        d.Add(b);
        d.Add(c);

        Assert.Equal([b], d.FilterByExtension(".md"));
        Assert.Equal([a, c], d.FilterByExtension(".txt"));
        Assert.Equal([], d.FilterByExtension(".cs"));
    }
}
