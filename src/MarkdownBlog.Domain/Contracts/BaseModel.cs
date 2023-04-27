using MarkdownBlog.Domain.Utils;

namespace MarkdownBlog.Domain.Contracts;

public abstract class BaseModel
{
    public BaseModel()
    {
        Id = Guid.NewGuid().ToShortString();
    }
    public string Id { get; set; }
}
