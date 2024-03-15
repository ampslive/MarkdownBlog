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
        group.MapGet("/{id}", GetAuthor);
        group.MapPost("/", CreateAuthor);
        group.MapPut("/{id}", UpdateAuthor);
        group.MapDelete("/{id}", RemoveAuthor);
    }
    
    public async static Task<Results<Ok<List<Author>>, NotFound>> GetAuthors(BlobServiceClient blobServiceClient,
        IBlobContext<BlogMaster> context,
        BlobStoreHelper blobStoreHelper,
        AuthorStore store)
    {
        var result = await store.GetAuthors();

        return (result != null) ? TypedResults.Ok(result)
        : TypedResults.NotFound();
    }

    public async static Task<Results<Ok<Author>, NotFound>> GetAuthor(BlobServiceClient blobServiceClient,
        AuthorStore store,
        string id)
    {
        var result = await store.GetAuthors(id);

        return (result.Count != 0) ? TypedResults.Ok(result.First())
        : TypedResults.NotFound();
    }

    public async static Task<Created<Author>> CreateAuthor(BlobServiceClient blobServiceClient,
        AuthorStore store,
        [FromBody] AuthorModel model)
    {
        var author = await store.AddAuthor(model.Name, model.ImageUri, model.Bio, model.Socials);
        
        return TypedResults.Created($"{author?.Id}", author);
    }

    public async static Task<Results<Ok<Author>, NotFound>> UpdateAuthor(BlobServiceClient blobServiceClient,
        AuthorStore store,
        string id,
        [FromBody] AuthorModel model)
    {
        var author = await store.UpdateAuthor(id, model.Name, model.ImageUri, model.Bio, model.Socials);

        return (author != null) ? TypedResults.Ok(author)
            : TypedResults.NotFound();
    }

    public async static Task<Results<Ok<Author>, NotFound>> RemoveAuthor(BlobServiceClient blobServiceClient,
        AuthorStore store,
        string id)
    {
        var author = await store.RemoveAuthor(id);

        return (author != null) ? TypedResults.Ok(author)
            : TypedResults.NotFound();
    }

}
