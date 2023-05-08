using Azure.Storage.Blobs;
using System.Reflection.Metadata;
using System.Text;

namespace MarkdownBlog.Infra;

public class BlobStoreHelper
{
    public string ContainerName { get; set; } = "bm1";
    public string FileName { get; set; } = "blogMaster.json";
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _containerClient;
    private readonly BlobClient _blobClient;

    public BlobStoreHelper(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
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
