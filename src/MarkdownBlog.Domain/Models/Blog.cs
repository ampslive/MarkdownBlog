using MarkdownBlog.Domain.Contracts;

namespace MarkdownBlog.Domain.Models;

public class Blog : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Post> Posts { get; set; }
}
