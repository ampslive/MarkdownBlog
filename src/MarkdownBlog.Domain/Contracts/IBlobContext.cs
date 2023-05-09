namespace MarkdownBlog.Domain.Contracts;

public interface IBlobContext<T>
{
    Task SaveAsync(T obj);
    Task<T?> LoadAsync();
}
