using Azure.Storage.Blobs;
using System.Reflection.Metadata;
using System.Text;

namespace MarkdownBlog.Infra;

public class BlobStoreHelper
{
    private readonly string _containerName;
    private readonly string _fileName;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _containerClient;
    private readonly BlobClient _blobClient;

    public BlobStoreHelper(BlobServiceClient blobServiceClient, string containerName, string fileName)
    {
        _blobServiceClient = blobServiceClient;
        _containerName = containerName;
        _fileName = fileName;
        _containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        _blobClient = _containerClient.GetBlobClient(_fileName);
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
