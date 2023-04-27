using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using MarkdownBlog.Domain.Models;

public static class BlogGroup
{
    public static IEndpointRouteBuilder MapBlogApi(this IEndpointRouteBuilder group)
    {
        group.MapGet("/posts", GetPosts);
        group.MapPost("/posts", Results.Ok(CreatePost));
        return group;
    }

    public static string GetPosts()
    {
        return "Hello Blog";
        //return TypedResults.Created($"{todo.Id}", todo);
    }

    public static Task CreatePost(Blog blog)
    {
        return Task.CompletedTask;
    }
}

