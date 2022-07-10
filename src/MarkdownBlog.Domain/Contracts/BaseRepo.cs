namespace MarkdownBlog.Domain.Contracts;
public abstract class BaseRepo<T> : IRepository<T>
{
    public virtual T Create(T entity)
    {
        throw new NotImplementedException();
    }
}
