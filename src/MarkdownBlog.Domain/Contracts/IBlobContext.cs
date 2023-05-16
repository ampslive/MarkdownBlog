namespace MarkdownBlog.Domain.Contracts;

public interface IBlobContext<T>
{
    T Data { get; set; }
    Task SaveAsync(T obj);
}
