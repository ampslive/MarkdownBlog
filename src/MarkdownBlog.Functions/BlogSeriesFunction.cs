using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MarkdownBlog.Domain.Store;
using MarkdownBlog.Functions.Models;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MarkdownBlog.Infra;
using Microsoft.AspNetCore.Routing;

namespace MarkdownBlog.Functions
{
    public class BlogSeriesFunction
    {
        private readonly BlogSeriesStore _store;
        private readonly JsonSerializerOptions _serializerOptions;

        public BlogSeriesFunction(BlogSeriesStore store, IOptions<JsonSerializerOptions> serializerOptions)
        {
            _store = store;
            _serializerOptions = serializerOptions.Value;
        }

        [FunctionName("BlogSeriesGet")]
        public async Task<IActionResult> BlogSeriesGet(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "series")] HttpRequest req,
            ILogger log)
        {
            var result = await _store.Get();

            return (result != null) ? 
                new OkObjectResult(result)
            : new NotFoundObjectResult(result);
        }

        [FunctionName("BlogSeriesGetById")]
        public async Task<IActionResult> BlogSeriesGetById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "series/{id}")] HttpRequest req,
            ILogger log)
        {
            string id = req.Query["id"];

            var result = await _store.Get(id);

            return (result != null) ?
                new OkObjectResult(result)
            : new NotFoundObjectResult(id);
        }

        [FunctionName("BlogSeriesCreate")]
        public async Task<IActionResult> BlogSeriesCreate(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "series")] HttpRequest req,
            ILogger log)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            var model = JsonSerializer.Deserialize<BlogSeriesModel>(content, _serializerOptions);

            var result = await _store.Add(model.Title, model.Description);

            return new CreatedResult($"{result?.Id}", result);
        }

        [FunctionName("BlogSeriesUpdate")]
        public async Task<IActionResult> BlogSeriesUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "series/{id}")] HttpRequest req,
            ILogger log)
        {
            string id = req.HttpContext.GetRouteValue("id").ToString();

            var content = await new StreamReader(req.Body).ReadToEndAsync();
            var model = JsonSerializer.Deserialize<BlogSeriesModel>(content, _serializerOptions);

            var result = await _store.Update(id, model.Title, model.Description);

            return (result != null) ?
                new OkObjectResult(result)
            : new NotFoundObjectResult(id);
        }

        [FunctionName("BlogSeriesDelete")]
        public async Task<IActionResult> BlogSeriesDelete(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "series/{id}")] HttpRequest req,
            ILogger log)
        {
            string id = req.HttpContext.GetRouteValue("id").ToString();

            var result = await _store.Remove(id);

            return (result != null) ?
                new OkObjectResult(result)
            : new NotFoundObjectResult(id);
        }
    }
}
