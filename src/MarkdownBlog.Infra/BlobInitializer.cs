using Azure.Storage.Blobs;
using MarkdownBlog.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MarkdownBlog.Infra;

public static class BlobInitializer
{
    public async static void CreateContainerAndBlob(IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            try
            {
                var blobServiceClient = services.GetRequiredService<BlobServiceClient>();
                var blobStoreHelper = services.GetRequiredService<BlobStoreHelper>();
                var containerClient = blobServiceClient.GetBlobContainerClient(blobStoreHelper.ContainerName);
                containerClient.CreateIfNotExists();
                var blobClient = containerClient.GetBlobClient(blobStoreHelper.FileName);

                if (!blobClient.Exists())
                {
                    var data = JsonSerializer.Serialize(BlogMaster.Default());
                    await blobStoreHelper.CreateBlobAsync(data);
                }
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "An error occurred while seeding blob container");
            }
        }
    }
}
