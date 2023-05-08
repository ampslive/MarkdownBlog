using MarkdownBlog.Domain.Models;

namespace MarkdownBlog.Domain.Store;

public class BlogStore
{
    public BlogStore()
    {
    }

    public async Task<Blog> Add(Blog blog)
    {
        throw new NotImplementedException();
    }

    public async Task<Blog> Get(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Blog>> GetBlogByTitle(string text)
    {
        throw new NotImplementedException();
    }
}
