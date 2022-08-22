namespace MarkdownBlog.Domain.Contracts;

public abstract class BaseRepo<T> : IRepository<T>
{
    public abstract string TableName { get; set; }

    private readonly IDatabaseHelper dbHelper;

    public BaseRepo(IDatabaseHelper helper)
    {
        dbHelper = helper;
    }

    public virtual async Task<T> Create(T entity)
    {
        return await dbHelper.CreateAsync(entity, this.TableName);
    }
}
