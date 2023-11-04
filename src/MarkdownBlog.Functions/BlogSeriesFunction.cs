using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MarkdownBlog.Domain.Store;

namespace MarkdownBlog.Functions
{
    public class BlogSeriesFunction
    {
        private readonly BlogSeriesStore _store;

        public BlogSeriesFunction(BlogSeriesStore store)
        {
            _store = store;
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

    }
}
