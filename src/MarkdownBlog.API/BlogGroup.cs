using Azure.Storage.Blobs;
using MarkdownBlog.API.Models;
using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;
using MarkdownBlog.Domain.Store;
using MarkdownBlog.Infra;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public static class BlogGroup
{
    public static IEndpointRouteBuilder MapBlogApi(this IEndpointRouteBuilder group)
    {
        group.MapGet("/", () => "Hello MD Blog");

        //author
        group.MapPost("/author", CreateAuthor);
        return group;
    }

    public async static Task<Created<BlogMaster>> CreateAuthor(BlobServiceClient blobServiceClient, 
        IBlobContext<BlogMaster> context, 
        [FromBody] AuthorModel model)
    {
        var store = new BlogMasterStore(context);

        var bm = await store.GetBlogMaster();

        var author = await store.AddAuthor(bm, model.Name, model.ImageUri, model.Bio);
        
        return TypedResults.Created($"{author.Authors.Count}", author);
    }
}

