using Azure.Storage.Blobs;
using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;
using System.Text.Json;

namespace MarkdownBlog.Infra;

public class Context : IBlobContext<BlogMaster>
{
    private readonly BlobStoreHelper _blobHelper;

    public Context(BlobServiceClient blobServiceClient, BlobStoreHelper blobStoreHelper)
    {
        _blobHelper = blobStoreHelper;
    }


    public async Task SaveAsync(BlogMaster obj)
    {
        var json = JsonSerializer.Serialize(obj);
        await _blobHelper.CreateBlobAsync(json);
    }

    public async Task<BlogMaster?> LoadAsync()
    {
        var result = await _blobHelper.GetBlobAsync();

        if (result is null)
        {
            return new BlogMaster
            {
                Authors = new List<Author>(),
                Blogs = new List<Blog>(),
                BlogSeries = new List<BlogSeries>()
            };
        }
        return JsonSerializer.Deserialize<BlogMaster>(result);

    }
}
