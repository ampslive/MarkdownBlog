using Azure.Storage.Blobs;
using MarkdownBlog.API.Models;
using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;
using MarkdownBlog.Domain.Store;
using MarkdownBlog.Infra;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MarkdownBlog.API.Endpoints;

public static class PostEndpoints
{
    public static void MapPostEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/posts").WithTags(nameof(Post));

        group.MapGet("/", GetPosts);
        group.MapGet("/{id}", GetPosts);
        group.MapPost("/", CreatePost);
        //group.MapPut("/{id}", UpdateAuthor);
        //group.MapDelete("/{id}", RemoveAuthor);
    }

    public async static Task<Results<Ok<List<Blog>>, NotFound>> GetPosts(BlobServiceClient blobServiceClient,
        PostStore store,
        string? id)
    {
        var result = await store.GetPosts(id);

        return (result != null) ? TypedResults.Ok(result)
        : TypedResults.NotFound();
    }

    public async static Task<Created<Post>> CreatePost(BlobServiceClient blobServiceClient,
        PostStore store,
        [FromBody] PostModel model)
    {
        var post = new Post()
        {
            Title = model.Title,
            BannerUri = model.BannerUri,
            Description = model.Description,
            Body = model.Body,
            DateCreated = DateTime.UtcNow,
            Authors = model.Authors
        };
        var blog = await store.AddPost(model.BlogSeriesId, post);

        return TypedResults.Created($"{post?.Id}", post);
    }
}
