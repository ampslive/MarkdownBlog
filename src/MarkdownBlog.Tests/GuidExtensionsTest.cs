using MarkdownBlog.Domain.Utils;
using Xunit;

namespace MarkdownBlog.Tests;

public class GuidExtensionsTest
{
    [Fact]
    public void Guid_Create_ShortGuid()
    {
        var result = Guid.NewGuid().ToShortString();
        Assert.NotNull(result);
    }

    [Fact]
    public void Guid_Create_ShortGuid_NotContainPlus()
    {
        var result = Guid.NewGuid().ToShortString();
        Assert.DoesNotContain("+", result);
    }

    [Fact]
    public void Guid_Create_ShortGuid_NotContainSlash()
    {
        var result = Guid.NewGuid().ToShortString();
        Assert.DoesNotContain("/", result);
    }
}
