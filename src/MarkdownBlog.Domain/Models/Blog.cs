using MarkdownBlog.Domain.Contracts;

namespace MarkdownBlog.Domain.Models;

public class Blog : BaseModel
{
    public string Name { get; set; }
    public List<IPost> Posts { get; set; }
}
