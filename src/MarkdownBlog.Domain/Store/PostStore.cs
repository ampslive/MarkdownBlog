﻿using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;
using MarkdownBlog.Domain.Utils;
using System.Reflection;

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

    public async Task<List<Post>?> GetPosts(string? id = null)
    {
        if (id is null)
            return _data?.Posts;

        return _data?.Posts.Where(p => p.Id == id).ToList();
    }

    public async Task<List<Post>?> GetPostsByStatus(PostStatus postStatus)
    {
        return _data?.Posts.Where(p => p.Status == postStatus).ToList();
    }

    public async Task<Post?> AddPost(Post post)
    {
        if (_data is null)
            return null;


        _data.Posts.Add(post);
        await _context.SaveAsync(_data);


        return post;
    }

    public async Task<Post?> UpdatePost(string postId, PostStatus postStatus)
    {
        if (_data is null)
            return null;

        var postToUpdate = _data?.Posts.FirstOrDefault(p => p.Id == postId);

        if (postToUpdate is null)
            return null;

        postToUpdate.UpdatePostStatus(postStatus);
        await _context.SaveAsync(_data);

        return postToUpdate;
    }

    public async Task<Post?> UpdatePost(string postId, Post post)
    {
        if (_data is null)
            return null;

        var postToUpdate = _data?.Posts.FirstOrDefault(p => p.Id == postId);

        if (postToUpdate is null)
            return null;

        postToUpdate.Title = post.Title;
        postToUpdate.BannerUri = post.BannerUri;
        postToUpdate.AuthorIds = post.AuthorIds;
        postToUpdate.Body = post.Body;
        postToUpdate.Description = post.Description;
        postToUpdate.Meta = post.Meta;
        postToUpdate.SeriesId = post.SeriesId;

        await _context.SaveAsync(_data);

        return postToUpdate;
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
    public async Task<Post?> RemovePost(string id)
    {
        if (_data is null)
            return null;

        var postToRemove = _data?.Posts.FirstOrDefault(p => p.Id == id);

        if (postToRemove is null)
            return null;

        _data?.Posts.Remove(postToRemove);
        await _context.SaveAsync(_data);

        return postToRemove;
    }


}
