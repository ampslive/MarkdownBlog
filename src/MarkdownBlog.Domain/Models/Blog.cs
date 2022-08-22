using MarkdownBlog.Domain.Contracts;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MarkdownBlog.Domain.Models
{
    public class Blog
    {
        //[JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<IPost> Posts { get; set; }
    }
}
