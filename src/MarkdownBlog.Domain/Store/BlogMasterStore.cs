using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;

namespace MarkdownBlog.Domain.Store;

public class BlogMasterStore
{
    private readonly IBlobContext<BlogMaster> _context;
    private BlogMaster? _data;
    public BlogMasterStore(IBlobContext<BlogMaster> context)
    {
        _context = context;
    }

    public async Task<Author?> AddAuthor(string name, string imageUri, string bio)
    {
        _data = await _context.LoadAsync();

        if (_data is null)
            return null;
        
        _data?.Authors.Add(new Author { Name = name, ImageUri = imageUri, Bio = bio });
        await _context.SaveAsync(_data);

        return _data?.Authors.Last();
    }

    public async Task<Author?> RemoveAuthor(string id)
    {
        _data = await _context.LoadAsync();

        if (_data is null)
            return null;

        var author = _data?.Authors.FirstOrDefault(x => x.Id == id);

        if (author is null)
            return null;

        _data?.Authors.Remove(author);
        await _context.SaveAsync(_data);

        return author;
    }

    public async Task<List<Author>?> GetAuthors()
    {
        _data = await _context.LoadAsync();
        return _data?.Authors;
    }
}
