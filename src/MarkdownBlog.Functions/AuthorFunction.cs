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

    [Function("AuthorFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "delete", Route = "authors/{id?}")] HttpRequestData req)
    {
        HttpResponseData response = req.Method switch
        {
            "GET" => await GetAuthors(req),
            "POST" => await CreateAuthor(req),
            "PUT" => await UpdateAuthor(req),
            "DELETE" => await DeleteAuthor(req),
            _ => req.CreateResponse(HttpStatusCode.MethodNotAllowed)
        };

        return response;
    }

    public async Task<HttpResponseData> GetAuthors(HttpRequestData req)
    {
        string? id = req.Url.Segments.Length > 3 ? req.Url.Segments[3] : default;
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

    public async Task<HttpResponseData> CreateAuthor(HttpRequestData req)
    {
        var content = await new StreamReader(req.Body).ReadToEndAsync();
        var model = JsonSerializer.Deserialize<AuthorRequest>(content, _serializerOptions);

        var result = await _store.AddAuthor(model.Name, model.ImageUri, model.Bio, model.Socials);

        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(result);

        return response;

    }

    public async Task<HttpResponseData> UpdateAuthor(HttpRequestData req)
    {
        string? id = req.Url.Segments.Length > 3 ? req.Url.Segments[3] : default;

        var content = await new StreamReader(req.Body).ReadToEndAsync();
        var model = JsonSerializer.Deserialize<AuthorRequest>(content, _serializerOptions);

        var result = await _store.UpdateAuthor(id, model.Name, model.ImageUri, model.Bio, model.Socials);

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

    public async Task<HttpResponseData> DeleteAuthor(HttpRequestData req)
    {
        string? id = req.Url.Segments.Length > 3 ? req.Url.Segments[3] : default;

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
