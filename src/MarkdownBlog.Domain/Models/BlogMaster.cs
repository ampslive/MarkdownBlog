using System.ComponentModel;

namespace MarkdownBlog.Domain.Models;

public class BlogMaster
{
    public List<Author> Authors { get; set; }
    public List<BlogSeries> BlogSeries { get; set; }
    public List<Blog> Blogs { get; set; }

    public static BlogMaster Default()
    {
        return new BlogMaster()
        {
            Authors = new List<Author>(),
            BlogSeries = new List<BlogSeries>(),
            Blogs = new List<Blog>()
        };
    }
}
