using MarkdownBlog.Domain.Contracts;

namespace MarkdownBlog.Domain.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<IPost> Posts { get; set; }
    }
}
