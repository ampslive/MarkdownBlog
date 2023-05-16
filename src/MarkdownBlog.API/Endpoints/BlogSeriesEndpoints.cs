using Azure.Storage.Blobs;
using MarkdownBlog.API.Models;
using MarkdownBlog.Domain.Models;
using MarkdownBlog.Domain.Store;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MarkdownBlog.API.Endpoints;

public static class BlogSeriesEndpoints
{
    public static void MapBlogSeriesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/series").WithTags(nameof(BlogSeries));

        group.MapGet("/", GetSeries);
        group.MapGet("/{id}", GetSeriesById);
        group.MapPost("/", CreateSeries);
        group.MapPut("/{id}", UpdateSeries);
        group.MapDelete("/{id}", RemoveSeries);

        //return group;
    }

    public async static Task<Results<Ok<List<BlogSeries>>, NotFound>> GetSeries(BlobServiceClient blobServiceClient,
        BlogSeriesStore store)
    {
        var result = await store.Get();

        return (result != null) ? TypedResults.Ok(result)
        : TypedResults.NotFound();
    }

    public async static Task<Results<Ok<BlogSeries>, NotFound>> GetSeriesById(BlobServiceClient blobServiceClient,
        BlogSeriesStore store,
        string id)
    {
        var result = await store.Get(id);

        return (result.Count != 0) ? TypedResults.Ok(result.First())
        : TypedResults.NotFound();
    }

    public async static Task<Created<BlogSeries>> CreateSeries(BlobServiceClient blobServiceClient,
        BlogSeriesStore store,
        [FromBody] BlogSeriesModel model)
    {
        var result = await store.Add(model.Title, model.Description);

        return TypedResults.Created($"{result?.Id}", result);
    }

    public async static Task<Results<Ok<BlogSeries>, NotFound>> UpdateSeries(BlobServiceClient blobServiceClient,
        BlogSeriesStore store,
        string id,
        [FromBody] BlogSeriesModel model)
    {
        var result = await store.Update(id, model.Title, model.Description);

        return (result != null) ? TypedResults.Ok(result)
            : TypedResults.NotFound();
    }

    public async static Task<Results<Ok<BlogSeries>, NotFound>> RemoveSeries(BlobServiceClient blobServiceClient,
        BlogSeriesStore store,
        string id)
    {
        var result = await store.Remove(id);

        return (result != null) ? TypedResults.Ok(result)
            : TypedResults.NotFound();
    }
}

