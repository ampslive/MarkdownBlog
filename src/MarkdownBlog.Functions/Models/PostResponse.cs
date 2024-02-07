using MarkdownBlog.Domain.Models;

namespace MarkdownBlog.Functions.Models;

public class PostResponse
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string BannerUri { get; set; }
    public string Description { get; set; }
    public string Body { get; set; }
    public DateTime DateCreated { get; set; }
    public List<string> AuthorIds { get; set; }
    public BlogSeries? Series { get; set; }
    public Meta Meta { get; set; }
}
