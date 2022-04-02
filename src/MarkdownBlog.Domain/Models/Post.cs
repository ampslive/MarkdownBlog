using MarkdownBlog.Domain.Contracts;

namespace MarkdownBlog.Domain.Models
{
    public class Post : IPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Permalink { get; set; }
        public string ContentHTML { get; set; }
        public string ContentMarkDown { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; }
        public PostStatus Status { get; set; }
        public List<Author> Authors { get; set; }
    }
}