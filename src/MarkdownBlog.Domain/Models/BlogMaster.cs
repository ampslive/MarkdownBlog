namespace MarkdownBlog.Domain.Models;

public class BlogMaster
{
    public List<Author> Authors { get; set; }
    public List<BlogSeries> BlogSeries { get; set; }
    public List<Blog> Blogs { get; set; }
}
