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

    public BlogSeriesFunction(BlogSeriesStore store, IOptions<JsonSerializerOptions> serializerOptions)
    {
        _store = store;
        _serializerOptions = serializerOptions.Value;
    }

    [Function("BlogSeriesGet")]
    public async Task<HttpResponseData> BlogSeriesGet(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "series")] HttpRequestData req,
        ILogger log)
    {
        var result = await _store.Get();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result);

        return response;
    }
    
    [Function("BlogSeriesGetById")]
    public async Task<HttpResponseData> BlogSeriesGetById(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "series/{id}")] HttpRequestData req,
        ILogger log)
    {
        string id = req.Url.Segments[3];

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
    [Function("BlogSeriesCreate")]
    public async Task<HttpResponseData> BlogSeriesCreate(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "series")] HttpRequestData req,
        ILogger log)
    {
        var content = await new StreamReader(req.Body).ReadToEndAsync();
        var model = JsonSerializer.Deserialize<BlogSeriesModel>(content, _serializerOptions);

        var result = await _store.Add(model.Title, model.Description);

        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(result);

        return response;
    }

    [Function("BlogSeriesUpdate")]
    public async Task<HttpResponseData> BlogSeriesUpdate(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "series/{id}")] HttpRequestData req,
        ILogger log)
    {
        string id = req.Url.Segments[3]; ;

        var content = await new StreamReader(req.Body).ReadToEndAsync();
        var model = JsonSerializer.Deserialize<BlogSeriesModel>(content, _serializerOptions);

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


    [Function("BlogSeriesDelete")]
    public async Task<HttpResponseData> BlogSeriesDelete(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "series/{id}")] HttpRequestData req,
        ILogger log)
    {
        string id = req.Url.Segments[3]; ;

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
