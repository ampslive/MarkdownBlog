using MarkdownBlog.API.Models;
using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;
using MarkdownBlog.Domain.Store;
using MarkdownBlog.Functions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Dynamic;
using System.Net;
using System.Text.Json;

namespace MarkdownBlog.Functions;

public class PostFunction
{
    private readonly PostStore _store;
    private readonly BlogSeriesStore _storeSeries;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly ILogger _logger;

    public PostFunction(PostStore store, BlogSeriesStore storeSeries, IOptions<JsonSerializerOptions> serializerOptions, ILoggerFactory loggerFactory)
    {
        _store = store;
        _storeSeries = storeSeries;
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

    private async Task<PostResponse?> UpdatePost(HttpRequestData req, string id)
    {
        var content = await new StreamReader(req.Body).ReadToEndAsync();
        var model = JsonSerializer.Deserialize<PostRequest>(content, _serializerOptions);

        var postToUpdate = new Post { 
            Title = model.Title,
            BannerUri = model.BannerUri,
            AuthorIds = model.AuthorIds,
            Body = model.Body,
            Description = model.Description,
            Meta = model.Meta,
            SeriesId = model.SeriesId
        };

        var postUpdated = await _store.UpdatePost(id, postToUpdate);

        var blogSeries = await _storeSeries.Get();

        return PostResponse.Convert(postUpdated, blogSeries);
    }

    private async Task<PostResponse?> UpdatePostStatus(string id, PostStatus status)
    {
        var post = await _store.UpdatePost(id, status);
        var blogSeries = await _storeSeries.Get();
        
        return PostResponse.Convert(post, blogSeries);
    }

    private async Task<HttpResponseData> DeletePost(HttpRequestData req)
    {
        string? id = req.Url.Segments.Length > 3 ? req.Url.Segments[3] : default;

        var result = await _store.RemovePost(id);

        if (result == null)
        {
            var responseNotFound = req.CreateResponse(HttpStatusCode.NotFound);
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
            SeriesId = model.SeriesId
        };

        var result = await _store.AddPost(post);
        var blogSeries = await _storeSeries.Get();

        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(PostResponse.Convert(result, blogSeries));

        return response;
    }

    private async Task<List<PostResponse>?> GetPosts(string? id)
    {
        var postsList = new List<PostResponse>();

        var posts = await _store.GetPosts(id);

        var blogSeries = await _storeSeries.Get();

        foreach(var p in posts)
        {
            postsList.Add(PostResponse.Convert(p, blogSeries));
        }

        return postsList;
    }

    private async Task<List<PostResponse>?> GetPostsByFilter(PostStatus status)
    {
        var postsList = new List<PostResponse>();

        var posts = await _store.GetPostsByStatus(status);

        var blogSeries = await _storeSeries.Get();

        foreach (var p in posts)
        {
            postsList.Add(PostResponse.Convert(p, blogSeries));
        }

        return postsList;
    }
}
