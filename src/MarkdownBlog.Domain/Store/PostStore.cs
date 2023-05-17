using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;

namespace MarkdownBlog.Domain.Store;

public class PostStore
{
    private readonly IBlobContext<BlogMaster> _context;
    private BlogMaster? _data;
    public PostStore(IBlobContext<BlogMaster> context)
    {
        _context = context;
        _data = context.Data;
    }

    public async Task<List<Blog>?> GetPosts(string? id = null)
    {
        if (id is null)
            return _data?.Blogs;

        return _data?.Blogs.SelectMany(b => b.Posts.Where(p => p.Id == id), 
            (b, p) => new Blog
            {
                Id = b.Id,
                Description = b.Description,
                Title = b.Title,
                Posts = new List<Post>() { p }
            }).ToList();
    }

    public async Task<Blog?> AddPost(string blogSeriesId, Post post)
    {
        if (_data is null)
            return null;

        var blogToAdd = _data?.Blogs.FirstOrDefault(b=>b.Id == blogSeriesId);
        
        if (blogToAdd is null)
        {
            var blogSeries = _data.BlogSeries.FirstOrDefault(bs => bs.Id == blogSeriesId);
            _data.Blogs.Add(new Blog { 
                Id = blogSeries.Id,
                Title = blogSeries.Title,
                Posts = new List<Post>() { post }
            });
            await _context.SaveAsync(_data);
            return _data?.Blogs.Last();
        }

        blogToAdd?.Posts.Add(post);

        await _context.SaveAsync(_data);

        return blogToAdd;
    }

    //TODO
    /*
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
    */
    public async Task<Blog?> RemovePost(string id)
    {
        if (_data is null)
            return null;

        var blog = _data?.Blogs.SelectMany(b => b.Posts.Where(p => p.Id == id),
            (b, p) => new Blog
            {
                Id = b.Id,
                Description = b.Description,
                Title = b.Title,
                Posts = new List<Post>() { p }
            }).FirstOrDefault();

        if (blog is null)
            return null;

        var postToRemove = blog.Posts.FirstOrDefault(x => x.Id == id);

        _data?.Blogs.Select(x=>x.Posts.Remove(postToRemove));
        await _context.SaveAsync(_data);

        return blog;
    }
}
