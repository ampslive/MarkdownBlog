using MarkdownBlog.Domain.Models;

namespace MarkdownBlog.Functions.Models;

public class BlogResponse
{
    public List<Author> Authors { get; set; }
    public List<BlogSeries> BlogSeries { get;}
    public List<PostResponse> Posts { get; set; }

    public BlogResponse(List<Author> authors, List<BlogSeries> blogSeries, List<PostResponse> posts)
    {
        Authors = authors;
        BlogSeries = blogSeries;
        Posts = posts;
    }
}
