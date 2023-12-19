using System.Net;
using MarkdownBlog.Domain.Store;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MarkdownBlog.API.Models;
using MarkdownBlog.Domain.Models;

namespace MarkdownBlog.Functions;

public class PostFunction
{
    private readonly PostStore _store;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly ILogger _logger;

    public PostFunction(PostStore store, IOptions<JsonSerializerOptions> serializerOptions, ILoggerFactory loggerFactory)
    {
        _store = store;
        _serializerOptions = serializerOptions.Value;
        _logger = loggerFactory.CreateLogger<PostFunction>();
    }

    [Function("PostFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", "delete", Route = "posts/{id?}")] HttpRequestData req)
    {
        HttpResponseData response = req.Method switch
        {
            "GET" => await GetPosts(req),
            "POST" => await CreatePost(req),
            "DELETE" => await DeletePost(req),
            _ => req.CreateResponse(HttpStatusCode.MethodNotAllowed)
        };

        return response;
    }

    private async Task<HttpResponseData> DeletePost(HttpRequestData req)
    {
        string? id = req.Url.Segments.Length > 3 ? req.Url.Segments[3] : default;

        var result = await _store.RemovePost(id);

        if (result == null)
        {
            var responseNotFound = req.CreateResponse(HttpStatusCode.NotFound);
            await responseNotFound.WriteAsJsonAsync(result);
            return responseNotFound;
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result);

        return response;
    }

    private async Task<HttpResponseData> CreatePost(HttpRequestData req)
    {
        var content = await new StreamReader(req.Body).ReadToEndAsync();
        var model = JsonSerializer.Deserialize<PostRequest>(content, _serializerOptions);

        var post = new Post()
        {
            Title = model.Title,
            BannerUri = model.BannerUri,
            Description = model.Description,
            Body = model.Body,
            DateCreated = DateTime.UtcNow,
            AuthorIds = model.AuthorIds,
            Meta = model.Meta,
            Series = model.Series
        };

        var result = await _store.AddPost(post);

        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(result);

        return response;
    }

    private async Task<HttpResponseData> GetPosts(HttpRequestData req)
    {
        string? id = req.Url.Segments.Length > 3 ? req.Url.Segments[3] : default;
        var result = await _store.GetPosts(id);

        if (result == null)
        {
            var responseNotFound = req.CreateResponse(HttpStatusCode.NotFound);
            await responseNotFound.WriteAsJsonAsync(result);
            return responseNotFound;
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result);

        return response;
    }
}
