namespace MarkdownBlog.Domain.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Permalink { get; set; }
        public string ContentHTML { get; set; }
        public string ContentMarkDown { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; }
        public List<Author> Authors { get; set; }
    }
}