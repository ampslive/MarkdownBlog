using MarkdownBlog.Domain.Models;

namespace MarkdownBlog.Domain.Contracts;

public interface IPost
{
    string Title { get; set; }
    string BannerUri { get; set; }
    string Description { get; set; }
    string Body { get; set; }
    DateTime DateCreated { get; set; }
    List<Guid> Authors { get; set; }
    Meta Meta { get; set; }
}
