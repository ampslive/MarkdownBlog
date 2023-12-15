using System.Net;
using MarkdownBlog.Domain.Store;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MarkdownBlog.API.Models;
using MarkdownBlog.Functions.Models;

namespace MarkdownBlog.Functions;

public class AuthorFunction
{
    private readonly AuthorStore _store;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly ILogger _logger;

    public AuthorFunction(AuthorStore store, IOptions<JsonSerializerOptions> serializerOptions, ILoggerFactory loggerFactory)
    {
        _store = store;
        _serializerOptions = serializerOptions.Value;
        _logger = loggerFactory.CreateLogger<AuthorFunction>();
    }

    [Function("AuthorGet")]
    public async Task<HttpResponseData> AuthorGet([HttpTrigger(AuthorizationLevel.Function, "get", Route = "authors")] HttpRequestData req)
    {
        var result = await _store.GetAuthors();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result);

        return response;
    }

    [Function("AuthorGetById")]
    public async Task<HttpResponseData> AuthorGetById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "author/{id}")] HttpRequestData req)
    {
        string id = req.Url.Segments[3];
        var result = await _store.GetAuthors(id);

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

    [Function("AuthorCreate")]
    public async Task<HttpResponseData> AuthorCreate([HttpTrigger(AuthorizationLevel.Function, "post", Route = "author")] HttpRequestData req)
    {
        var content = await new StreamReader(req.Body).ReadToEndAsync();
        var model = JsonSerializer.Deserialize<AuthorRequest>(content, _serializerOptions);

        var result = await _store.AddAuthor(model.Name, model.ImageUri, model.Bio);

        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(result);

        return response;

    }

    [Function("AuthorUpdate")]
    public async Task<HttpResponseData> AuthorUpdate(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "author/{id}")] HttpRequestData req,
        ILogger log)
    {
        string id = req.Url.Segments[3];

        var content = await new StreamReader(req.Body).ReadToEndAsync();
        var model = JsonSerializer.Deserialize<AuthorRequest>(content, _serializerOptions);

        var result = await _store.UpdateAuthor(id, model.Name, model.ImageUri, model.Bio);

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

    [Function("AuthorDelete")]
    public async Task<HttpResponseData> AuthorDelete(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "author/{id}")] HttpRequestData req,
        ILogger log)
    {
        string id = req.Url.Segments[3];

        var result = await _store.RemoveAuthor(id);

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
