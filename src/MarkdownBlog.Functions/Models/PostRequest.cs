using MarkdownBlog.Domain.Models;

namespace MarkdownBlog.API.Models;

public class PostRequest
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string BannerUri { get; set; }
    public string Description { get; set; }
    public string Body { get; set; }
    public DateTime DateCreated { get; set; }
    public List<string> AuthorIds { get; set; }
    public string SeriesId { get; set; }
    public Meta Meta { get; set; }
}


