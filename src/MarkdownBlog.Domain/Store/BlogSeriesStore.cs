using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace MarkdownBlog.Domain.Store;

public class BlogSeriesStore
{
    private readonly IBlobContext<BlogMaster> _context;
    private BlogMaster? _data;

    public BlogSeriesStore(IBlobContext<BlogMaster> context)
    {
        _context = context;
    }

    public async Task<List<BlogSeries>?> Get(string id = null)
    {
        _data = await _context.LoadAsync();

        if (id is null)
            return _data?.BlogSeries;

        return _data.BlogSeries.Where(x => x.Id == id).ToList();
    }

    public async Task<BlogSeries?> Add(string title, string desc)
    {
        _data = await _context.LoadAsync();

        if (_data is null)
            return null;

        _data?.BlogSeries.Add(new BlogSeries { Title = title, Description = desc });
        await _context.SaveAsync(_data);

        return _data?.BlogSeries.Last();
    }

    public async Task<BlogSeries?> Update(string id, string title, string desc)
    {
        _data = await _context.LoadAsync();

        if (_data is null)
            return null;

        var blogSeries = _data?.BlogSeries.FirstOrDefault(x => x.Id == id);

        if (blogSeries is null)
            return null;

        blogSeries.Title = title;
        blogSeries.Description = desc;

        await _context.SaveAsync(_data);

        return blogSeries;
    }

    public async Task<BlogSeries?> Remove(string id)
    {
        _data = await _context.LoadAsync();

        if (_data is null)
            return null;

        var blogSeries = _data?.BlogSeries.FirstOrDefault(x => x.Id == id);

        if (blogSeries is null)
            return null;

        _data?.BlogSeries.Remove(blogSeries);
        await _context.SaveAsync(_data);

        return blogSeries;
    }
}
