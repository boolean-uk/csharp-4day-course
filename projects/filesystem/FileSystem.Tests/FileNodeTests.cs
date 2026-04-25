using FileSystemApp;

namespace FileSystemApp.Tests;

public class FileNodeTests
{
    [Fact]
    public void Constructor_AssignsName()
    {
        FileNode f = new FileNode("readme.md", 100);
        Assert.Equal("readme.md", f.Name);
    }

    [Fact]
    public void Constructor_AssignsSize()
    {
        FileNode f = new FileNode("readme.md", 1234);
        Assert.Equal(1234, f.SizeBytes);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ThrowsOnBlankName(string name)
    {
        Assert.Throws<ArgumentException>(() => new FileNode(name, 1));
    }

    [Fact]
    public void Constructor_ThrowsOnNegativeSize()
    {
        Assert.Throws<ArgumentException>(() => new FileNode("x", -1));
    }

    [Fact]
    public void Constructor_AcceptsZeroSize()
    {
        FileNode f = new FileNode("empty.txt", 0);
        Assert.Equal(0, f.Size());
    }

    [Fact]
    public void Size_ReturnsOwnBytes()
    {
        FileNode f = new FileNode("readme.md", 1200);
        Assert.Equal(1200, f.Size());
    }

    [Fact]
    public void FileCount_IsAlwaysOne()
    {
        FileNode f = new FileNode("readme.md", 1200);
        Assert.Equal(1, f.FileCount());
    }

    [Fact]
    public void FindByName_ReturnsSelfWhenMatching()
    {
        FileNode f = new FileNode("readme.md", 100);
        Assert.Same(f, f.FindByName("readme.md"));
    }

    [Fact]
    public void FindByName_ReturnsNullWhenNotMatching()
    {
        FileNode f = new FileNode("readme.md", 100);
        Assert.Null(f.FindByName("other.md"));
    }

    [Fact]
    public void FindByName_IsCaseSensitive()
    {
        FileNode f = new FileNode("README.md", 100);
        Assert.Null(f.FindByName("readme.md"));
    }

    [Fact]
    public void LargestFile_ReturnsSelf()
    {
        FileNode f = new FileNode("readme.md", 100);
        Assert.Same(f, f.LargestFile());
    }

    [Fact]
    public void FilterByExtension_ReturnsSelfWhenMatching()
    {
        FileNode f = new FileNode("readme.md", 100);
        Assert.Equal([f], f.FilterByExtension(".md"));
    }

    [Fact]
    public void FilterByExtension_IsCaseInsensitive()
    {
        FileNode f = new FileNode("README.MD", 100);
        Assert.Equal([f], f.FilterByExtension(".md"));
    }

    [Fact]
    public void CountByExtension_ReturnsOneForExtension()
    {
        FileNode f = new FileNode("readme.md", 100);
        Assert.Equal(new Dictionary<string, int> { { ".md", 1 } }, f.CountByExtension());
    }

    [Fact]
    public void CountByExtension_IsCaseInsensitive()
    {
        FileNode f = new FileNode("README.MD", 100);
        Assert.Equal(new Dictionary<string, int> { { ".md", 1 } }, f.CountByExtension());
    }

    [Fact]
    public void Depth_ReturnsZero()
    {
        FileNode f = new FileNode("readme.md", 100);
        Assert.Equal(0, f.Depth());
    }
}
