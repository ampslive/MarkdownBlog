using MarkdownBlog.Domain.Contracts;

namespace MarkdownBlog.Domain.Models;

public class Post : IPost
{
    public string Title { get; set; }
    public string BannerUri { get; set; }
    public string Description { get; set; }
    public string Body { get; set; }
    public DateTime DateCreated { get; set; }
    public List<Guid> Authors { get; set; }
    public Meta Meta { get; set; }
}