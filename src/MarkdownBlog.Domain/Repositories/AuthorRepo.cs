using MarkdownBlog.Domain.Models;

namespace MarkdownBlog.Domain.Repositories;

public class AuthorRepo
{
    public Author Create(string name, string imageUri, string bio)
    {
        return new Author(name, imageUri, bio);
    }
}
