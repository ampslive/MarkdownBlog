using Azure.Storage.Blobs;
using MarkdownBlog.Domain.Models;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;

namespace MarkdownBlog.Infra;

public class BlobStoreHelper
{
    public readonly string ContainerName;
    public readonly string FileName;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _containerClient;
    private readonly BlobClient _blobClient;

    public BlobStoreHelper(BlobServiceClient blobServiceClient, string containerName, string fileName)
    {
        _blobServiceClient = blobServiceClient;
        ContainerName = containerName;
        FileName = fileName;
        _containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
        _blobClient = _containerClient.GetBlobClient(FileName);
    }

    public async Task CreateBlobAsync(string data)
    {
        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data)))
        {
            await _blobClient.UploadAsync(ms, true);
        }
    }

    public async Task<string> GetBlobAsync()
    {
        var blob = await _blobClient.DownloadContentAsync();
        return blob.Value.Content.ToString();
    }
}
