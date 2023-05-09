using Azure.Storage.Blobs;
using MarkdownBlog.API.Models;
using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;
using MarkdownBlog.Domain.Store;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MarkdownBlog.API.Endpoints;

public static class BlogEndpoints
{
    public static IEndpointRouteBuilder MapBlogEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/blog").WithTags(nameof(Blog));
        group.MapGet("/", () => "Hello MD Blog");
        
        return group;
    }
}

