using MarkdownBlog.Domain.Contracts;

namespace MarkdownBlog.Domain.Models;

public class BlogSeries : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
}
