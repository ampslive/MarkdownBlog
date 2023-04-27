namespace MarkdownBlog.Domain.Contracts;

public interface IDatabaseHelper
{
    Task<T> CreateAsync<T>(T obj, string tableName, string partitionKey);
    Task<T> GetAsync<T>(string id, string tableName, string partitionKey);
    Task<List<T>> QueryAsync<T>(string inputQuery, string tableName, string[] parameters);
}
