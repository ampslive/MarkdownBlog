using MarkdownBlog.API.Models;
using MarkdownBlog.Domain.Models;
using MarkdownBlog.Domain.Repositories;
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

    public static Created<Author> CreateAuthor([FromBody] AuthorModel model)
    {
        var store = new AuthorRepo();
        var author = store.Create(model.Name, model.ImageUri, model.Bio);
        return TypedResults.Created($"{author.Id}", author);
    }
}

