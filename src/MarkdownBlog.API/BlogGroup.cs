using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

public static class BlogGroup
{
    public static IEndpointRouteBuilder MapBlogApi(this IEndpointRouteBuilder group)
    {
        group.MapGet("/posts", GetPosts);
        return group;
    }

    public static string GetPosts()
    {
        return "Hello Blog";
        //return TypedResults.Created($"{todo.Id}", todo);
    }
}