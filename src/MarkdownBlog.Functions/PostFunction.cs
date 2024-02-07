using System.Net;
using MarkdownBlog.Domain.Store;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MarkdownBlog.API.Models;
using MarkdownBlog.Domain.Models;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using MarkdownBlog.Domain.Contracts;

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
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "delete", Route = "posts/{id?}/{postStatus?}")] HttpRequestData req, string? id, string? postStatus)
    {
        HttpResponseData response = req.Method switch
        {
            "GET" => await MapGetEndpoints(req, id),
            "POST" => await CreatePost(req),
            "DELETE" => await DeletePost(req),
            "PUT" => await MapUpdateEndpoints(req, id, postStatus),
            _ => req.CreateResponse(HttpStatusCode.MethodNotAllowed)
        };

        return response;
    }

    private async Task<HttpResponseData> MapGetEndpoints(HttpRequestData req, string id)
    {
        var result = id switch
        {
            "draft" => await GetPostsByFilter(PostStatus.Draft),
            "preview" => await GetPostsByFilter(PostStatus.Preview),
            "published" => await GetPostsByFilter(PostStatus.Published),
            "archive" => await GetPostsByFilter(PostStatus.Archive),
            _ => await GetPosts(id)
        };

        if (result?.Count == 0)
        {
            var responseNotFound = req.CreateResponse(HttpStatusCode.NotFound);
            await responseNotFound.WriteAsJsonAsync(result);
            return responseNotFound;
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result);

        return response;
    }

    private async Task<HttpResponseData> MapUpdateEndpoints(HttpRequestData req, string? id, string? status)
    {
        if (!Enum.TryParse(status, true, out PostStatus postStatus) && !String.IsNullOrEmpty(status))
        {
            var responseBadRequest = req.CreateResponse(HttpStatusCode.BadRequest);
            return responseBadRequest;
        }

        var post = (id, status) switch
        {
            ({ }, { }) => await UpdatePostStatus(id, postStatus),
            ({ }, null) => await UpdatePost(req, id),
            _ => null
        };

        if (post == null)
        {
            var responseBadRequest = req.CreateResponse(HttpStatusCode.NotFound);
            return responseBadRequest;
        }

        if (!String.IsNullOrEmpty(status) && post.Status != postStatus)
        {
            var responseBadRequest = req.CreateResponse(HttpStatusCode.RequestedRangeNotSatisfiable);
            return responseBadRequest;
        }

        var response = req.CreateResponse(HttpStatusCode.Accepted);
        await response.WriteAsJsonAsync(post);
        return response;
    }

    private async Task<Post?> UpdatePost(HttpRequestData req, string id)
    {
        throw new NotImplementedException();
    }

    private async Task<Post?> UpdatePostStatus(string id, PostStatus status)
    {
        return await _store.UpdatePost(id, status);
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

    private async Task<List<Post>?> GetPosts(string? id)
    {
        return await _store.GetPosts(id);
    }

    private async Task<List<Post>?> GetPostsByFilter(PostStatus status)
    {
        return await _store.GetPostsByStatus(status);
    }
}
