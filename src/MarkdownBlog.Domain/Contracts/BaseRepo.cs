using System.Collections.Concurrent;

namespace MarkdownBlog.Domain.Contracts;

public abstract class BaseRepo<T> : IRepository<T> where T : BaseModel
{
    public abstract string TableName { get; set; }

    private readonly IDatabaseHelper dbHelper;

    public BaseRepo(IDatabaseHelper helper)
    {
        dbHelper = helper;
    }

    public virtual async Task<T> Create(T entity, string? partitionKey = null)
    {
        if (partitionKey == null)
            partitionKey = entity.Id.ToString();

        return await dbHelper.CreateAsync(entity, this.TableName, partitionKey);
    }

    public virtual async Task<T> Get(string id, string? partitionKey = null)
    {
        if (partitionKey == null)
            partitionKey = id;

        return await dbHelper.GetAsync<T>(id, this.TableName, partitionKey);
    }

    public virtual async Task<List<T>> Search(string inputQuery, string[] parameters)
    {
        return await dbHelper.QueryAsync<T>(inputQuery, this.TableName, parameters);
    }
}
