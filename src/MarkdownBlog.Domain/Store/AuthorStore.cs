using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;

namespace MarkdownBlog.Domain.Store;

public class AuthorStore
{
    private readonly IBlobContext<BlogMaster> _context;
    private BlogMaster? _data;
    public AuthorStore(IBlobContext<BlogMaster> context)
    {
        _context = context;
        _data = context.Data;
    }

    public async Task<List<Author>?> GetAuthors(string? id = null)
    {
        if (id is null)
            return _data?.Authors;

        return _data?.Authors.Where(x => x.Id == id).ToList();
    }

    public async Task<Author?> AddAuthor(string name, string imageUri, string bio)
    {
        if (_data is null)
            return null;
        
        _data?.Authors.Add(new Author { Name = name, ImageUri = imageUri, Bio = bio });
        await _context.SaveAsync(_data);

        return _data?.Authors.Last();
    }

    public async Task<Author?> UpdateAuthor(string id, string name, string imageUri, string bio)
    {
        if (_data is null)
            return null;

        var author = _data?.Authors.FirstOrDefault(x => x.Id == id);

        if (author is null)
            return null;

        author.Name = name;
        author.ImageUri = imageUri;
        author.Bio = bio;

        await _context.SaveAsync(_data);

        return author;
    }

    public async Task<Author?> RemoveAuthor(string id)
    {
        if (_data is null)
            return null;

        var author = _data?.Authors.FirstOrDefault(x => x.Id == id);

        if (author is null)
            return null;

        _data?.Authors.Remove(author);
        await _context.SaveAsync(_data);

        return author;
    }
}
