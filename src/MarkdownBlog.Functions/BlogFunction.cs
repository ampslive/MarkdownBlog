using MarkdownBlog.Domain.Store;
using MarkdownBlog.Functions.Models;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace MarkdownBlog.Functions
{
    public class BlogFunction
    {
        private readonly ILogger<BlogFunction> _logger;
        private readonly BlogSeriesStore _blogSeriesStore;
        private readonly AuthorStore _authorStore;
        private readonly PostStore _postStore;

        public BlogFunction(ILogger<BlogFunction> logger, BlogSeriesStore blogSeriesStore, AuthorStore authorStore, PostStore postStore)
        {
            _logger = logger;
            _blogSeriesStore = blogSeriesStore;
            _authorStore = authorStore;
            _postStore = postStore;
        }

        [Function("BlogFunction")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "blog")] HttpRequestData req)
        {
            var authors = await _authorStore.GetAuthors();
            var posts = await _postStore.GetPosts();
            var blogSeries = await _blogSeriesStore.Get();

            var postResponses = new List<PostResponse>();

            foreach (var p in posts)
            {
                postResponses.Add(PostResponse.Convert(p, blogSeries));
            }

            var result = new BlogResponse(authors, blogSeries, postResponses);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);

            return response;
        }
    }
}
