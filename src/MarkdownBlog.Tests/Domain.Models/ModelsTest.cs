using MarkdownBlog.Domain.Models;
using Xunit;

namespace MarkdownBlog.Tests.Domain.Models;

public class ModelsTest
{
    [Theory]
    [InlineData("author-name", "author-img", "author-bio")]
    public void Author_Create_Success(string name, string img, string bio)
    {
        var author = new Author(name, img, bio);

        Assert.IsType<Author>(author);
        Assert.NotNull(author.Id);
        Assert.Equal(name, author.Name);
        Assert.Equal(bio, author.Bio);
        Assert.Equal(img, author.ImageUri);
    }
}
