using MarkdownBlog.Domain.Contracts;

namespace MarkdownBlog.Domain.Models;

public class Post : BaseModel, IPost
{
    public Post()
    {
        Status = PostStatus.Draft;
        DatePublished = null;
    }

    public string Title { get; set; }
    public string BannerUri { get; set; }
    public string Description { get; set; }
    public string Body { get; set; }
    public PostStatus Status { get; set; } 
    public DateTime? DatePublished { get; set; }
    public DateTime DateCreated { get; set; }
    public string SeriesId { get; set; }
    public List<string> AuthorIds { get; set; }
    public Meta Meta { get; set; }
}

