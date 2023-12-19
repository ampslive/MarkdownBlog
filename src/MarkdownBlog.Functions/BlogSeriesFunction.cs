using MarkdownBlog.Domain.Store;
using MarkdownBlog.Functions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace MarkdownBlog.Functions;

public class BlogSeriesFunction
{
    private readonly BlogSeriesStore _store;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly ILogger _logger;

    public BlogSeriesFunction(BlogSeriesStore store, IOptions<JsonSerializerOptions> serializerOptions, ILoggerFactory loggerFactory)
    {
        _store = store;
        _serializerOptions = serializerOptions.Value;
        _logger = loggerFactory.CreateLogger<BlogSeriesFunction>();
    }

    [Function("BlogSeriesFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "delete", Route = "series/{id?}")] HttpRequestData req)
    {
        HttpResponseData response = req.Method switch
        {
            "GET" => await GetBlogSeries(req),
            "POST" => await CreateBlogSeries(req),
            "PUT" => await UpdateBlogSeries(req),
            "DELETE" => await DeleteBlogSeries(req),
            _ => req.CreateResponse(HttpStatusCode.MethodNotAllowed)
        };

        return response;
    }

    public async Task<HttpResponseData> GetBlogSeries(HttpRequestData req)
    {
        string? id = req.Url.Segments.Length > 3 ? req.Url.Segments[3] : default;

        var result = await _store.Get(id);

        if(result == null)
        {
            var responseNotFound = req.CreateResponse(HttpStatusCode.NotFound);
            await responseNotFound.WriteAsJsonAsync(result);
            return responseNotFound;
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result);

        return response;
    }

    public async Task<HttpResponseData> CreateBlogSeries(HttpRequestData req)
    {
        var content = await new StreamReader(req.Body).ReadToEndAsync();
        var model = JsonSerializer.Deserialize<BlogSeriesRequest>(content, _serializerOptions);

        var result = await _store.Add(model.Title, model.Description);

        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(result);

        return response;
    }

    public async Task<HttpResponseData> UpdateBlogSeries(HttpRequestData req)
    {
        string? id = req.Url.Segments.Length > 3 ? req.Url.Segments[3] : default;

        var content = await new StreamReader(req.Body).ReadToEndAsync();
        var model = JsonSerializer.Deserialize<BlogSeriesRequest>(content, _serializerOptions);

        var result = await _store.Update(id, model.Title, model.Description);

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


    public async Task<HttpResponseData> DeleteBlogSeries(HttpRequestData req)
    {
        string? id = req.Url.Segments.Length > 3 ? req.Url.Segments[3] : default;

        var result = await _store.Remove(id);

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
