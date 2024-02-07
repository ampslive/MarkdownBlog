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
        group.MapPut("/{id}/{postStatus}", UpdatePostStatus);
        group.MapDelete("/{id}", RemovePost);
    }

    public async static Task<Results<Ok<List<Post>>, NotFound>> GetPosts(BlobServiceClient blobServiceClient,
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
            AuthorIds = model.AuthorIds,
            Meta = model.Meta,
            SeriesId = model.SeriesId
        };
        var blog = await store.AddPost(post);

        return TypedResults.Created($"{post?.Id}", post);
    }

    public async static Task<Results<Accepted<Post>, BadRequest>> UpdatePostStatus(BlobServiceClient blobServiceClient,
        PostStore store,
        string postId,
        PostStatus postStatus)
    {
        var post = await store.UpdatePost(postId, postStatus);

        return (post?.Status == postStatus) ? TypedResults.Accepted($"{post?.Id}", post)
            : TypedResults.BadRequest();
    }

    public async static Task<Results<Ok<Post>, NotFound>> RemovePost(PostStore store, string id)
    {
        var post = await store.RemovePost(id);

        return (post != null) ? TypedResults.Ok(post)
            : TypedResults.NotFound();
    }
}
