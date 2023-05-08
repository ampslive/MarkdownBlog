using Azure.Storage.Blobs;
using MarkdownBlog.Domain.Contracts;
using System.Text.Json;

namespace MarkdownBlog.Infra;

public class Context<T> : IBlobContext<T>
{
    private readonly BlobServiceClient _blobServiceClient;
    public Context(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }


    public async Task SaveAsync(T obj)
    {
        var json = JsonSerializer.Serialize(obj);
        var b = new BlobStoreHelper(_blobServiceClient);
        await b.CreateBlobAsync(json);
    }

    public async Task<T> LoadAsync()
    {
        var b = new BlobStoreHelper(_blobServiceClient);
        var result = await b.GetBlobAsync();
        return JsonSerializer.Deserialize<T>(result);
    }
}
