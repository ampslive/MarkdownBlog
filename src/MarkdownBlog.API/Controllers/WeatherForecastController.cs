using MarkdownBlog.Domain.Store;
using Microsoft.AspNetCore.Mvc;

namespace MarkdownBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly BlogStore blogStore;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config, BlogStore store)
        {
            _logger = logger;
            blogStore = store;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var blog = new Domain.Models.Blog
            {
                Id = Guid.NewGuid(),
                Name = $"Blog Title {Guid.NewGuid()}"
            };

            var result = await blogStore.Add(blog);
            var result2 = await blogStore.GetBlogByTitle(blog.Name.ToString());

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}