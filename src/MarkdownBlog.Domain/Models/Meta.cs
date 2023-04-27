using MarkdownBlog.Domain.Contracts;

namespace MarkdownBlog.Domain.Models;

public class Meta
{
    public ContentType ContentType { get; set; }
    public string ContentLocation { get; set; }
}
