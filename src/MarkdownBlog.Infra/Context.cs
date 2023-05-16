using Azure.Storage.Blobs;
using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Text.Json;

namespace MarkdownBlog.Infra;

public class Context : IBlobContext<BlogMaster>
{
    private readonly BlobStoreHelper _blobHelper;

    public BlogMaster Data { get; set; }

    public Context(BlobStoreHelper blobStoreHelper, BlogMaster data)
    {
        _blobHelper = blobStoreHelper;
        Data = data;
    }

    public async Task SaveAsync(BlogMaster obj)
    {
        var json = JsonSerializer.Serialize(obj);
        await _blobHelper.CreateBlobAsync(json);
    }
}
