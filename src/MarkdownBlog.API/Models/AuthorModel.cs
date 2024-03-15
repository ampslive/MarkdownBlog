using MarkdownBlog.Domain.Models;

namespace MarkdownBlog.API.Models;

public class AuthorModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ImageUri { get; set; }
    public string Bio { get; set; }
    public List<Social> Socials { get; set; }
}
