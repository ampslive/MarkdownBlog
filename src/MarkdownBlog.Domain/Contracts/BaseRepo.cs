namespace MarkdownBlog.Domain.Contracts;

public abstract class BaseRepo<T> : IRepository<T>
{
    public abstract string TableName { get; set; }
    public abstract string PartitionKey { get; set; }

    private readonly IDatabaseHelper dbHelper;

    public BaseRepo(IDatabaseHelper helper)
    {
        dbHelper = helper;
    }

    public virtual async Task<T> Create(T entity)
    {
        return await dbHelper.CreateAsync(entity, this.TableName, this.PartitionKey);
    }

    public virtual async Task<T> Get(string id)
    {
        return await dbHelper.GetAsync<T>(id, this.TableName, this.PartitionKey);
    }
}
