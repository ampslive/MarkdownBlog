using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;

namespace MarkdownBlog.Domain.Store;

public class BlogMasterStore
{
    private readonly IBlobContext<BlogMaster> _context;
    public BlogMasterStore(IBlobContext<BlogMaster> context)
    {
        _context = context;
    }

    public async Task<BlogMaster> GetBlogMaster()
    {
        return await _context.LoadAsync();
    }

    public async Task<BlogMaster> AddAuthor(BlogMaster blogMaster, string name, string imageUri, string bio)
    { 
        blogMaster.Authors.Add(new Author { Name = name, ImageUri = imageUri, Bio = bio });

        await _context.SaveAsync(blogMaster);
        return blogMaster;
    }
}
