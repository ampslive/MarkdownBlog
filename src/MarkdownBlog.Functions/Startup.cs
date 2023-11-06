using Azure.Storage.Blobs;
using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;
using MarkdownBlog.Domain.Store;
using MarkdownBlog.Infra;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Text.Json;


[assembly: FunctionsStartup(typeof(MarkdownBlog.Functions.Startup))]
namespace MarkdownBlog.Functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var context = builder.GetContext();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(context.ApplicationRootPath)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        builder.Services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(configuration["StorageConnectionString"]);
        });


        builder.Services.AddScoped<IBlobContext<BlogMaster>, Context>(serviceProvider =>
        {
            var blobStoreHelper = serviceProvider.GetRequiredService<BlobStoreHelper>();

            var data = JsonSerializer.Deserialize<BlogMaster>(blobStoreHelper.GetBlobAsync().Result);

            var ctx = new Context(blobStoreHelper, data);
            return ctx;
        });

        builder.Services.AddTransient<AuthorStore>();
        builder.Services.AddTransient<BlogSeriesStore>();
        builder.Services.AddTransient<PostStore>();
        builder.Services.AddSingleton<BlobStoreHelper>(serviceProvider =>
        {
            var blobServiceClient = serviceProvider.GetRequiredService<BlobServiceClient>();
            string containername = configuration["BlobContainerName"];
            string filename = configuration["BlobFileName"];
            return new BlobStoreHelper(blobServiceClient, containername, filename);
        });


        builder.Services.AddScoped(serviceProvider => 
        {
            BlobInitializer.CreateContainerAndBlob(serviceProvider);
            return serviceProvider.ToString();
        });

        builder.Services.Configure<JsonSerializerOptions>(options =>
        {
            options.PropertyNameCaseInsensitive = true;
        });
    }
}
