using MarkdownBlog.Domain.Contracts;
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
    public DateTime? DatePublished { get; set; }
    public PostStatus Status { get; set; }
    public List<string> AuthorIds { get; set; }
    public BlogSeries? Series { get; set; }
    public Meta Meta { get; set; }

    public static PostResponse Convert(Post post, List<BlogSeries> blogSeries)
    {
        return new PostResponse
        {
            Id = post.Id,
            Title = post.Title,
            BannerUri = post.BannerUri,
            Description = post.Description,
            Body = post.Body,
            DateCreated = post.DateCreated,
            DatePublished = post.DatePublished,
            Status = post.Status,
            AuthorIds = post.AuthorIds,
            Meta = post.Meta,
            Series = blogSeries.FirstOrDefault(x => x.Id.Equals(post.SeriesId))
        };
    }
}
