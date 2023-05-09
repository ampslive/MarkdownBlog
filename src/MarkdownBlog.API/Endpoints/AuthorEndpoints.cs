using Azure.Storage.Blobs;
using MarkdownBlog.API.Models;
using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;
using MarkdownBlog.Domain.Store;
using MarkdownBlog.Infra;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MarkdownBlog.API.Endpoints;

public static class AuthorEndpoints
{
    public static void MapAuthorEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/author").WithTags(nameof(Author));

        group.MapGet("/", GetAuthors);
        group.MapPost("/", CreateAuthor);
        group.MapDelete("/", RemoveAuthor);
    }
    
    public async static Task<Results<Ok<List<Author>>, NotFound>> GetAuthors(BlobServiceClient blobServiceClient,
        IBlobContext<BlogMaster> context,
        BlobStoreHelper blobStoreHelper,
        BlogMasterStore store)
    {
        var result = await store.GetAuthors();

        return (result != null) ? TypedResults.Ok(await store.GetAuthors())
        : TypedResults.NotFound();
    }

    public async static Task<Created<Author>> CreateAuthor(BlobServiceClient blobServiceClient,
        IBlobContext<BlogMaster> context,
        BlobStoreHelper blobStoreHelper,
        BlogMasterStore store,
        [FromBody] AuthorModel model)
    {
        var author = await store.AddAuthor(model.Name, model.ImageUri, model.Bio);
        
        return TypedResults.Created($"{author?.Id}", author);
    }

    public async static Task<Results<Ok<Author>, NotFound>> RemoveAuthor(BlobServiceClient blobServiceClient,
        IBlobContext<BlogMaster> context,
        BlobStoreHelper blobStoreHelper,
        BlogMasterStore store,
        [FromBody] AuthorModel model)
    {
        var author = await store.RemoveAuthor(model.Id);

        return (author != null) ? TypedResults.Ok(author)
            : TypedResults.NotFound();
    }

}
