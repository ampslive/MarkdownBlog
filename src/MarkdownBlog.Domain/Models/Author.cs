using MarkdownBlog.Domain.Contracts;

namespace MarkdownBlog.Domain.Models;

public class Author : BaseModel
{
    public string Name { get; set; }
    public string ImageUri { get; set; }
    public string Bio { get; set; }
}
