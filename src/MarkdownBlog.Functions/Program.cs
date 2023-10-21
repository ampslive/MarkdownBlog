using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Azure;
using MarkdownBlog.Domain.Store;
using MarkdownBlog.Infra;
using Microsoft.Extensions.Configuration;
using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Azure.Storage.Blobs;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(context.HostingEnvironment.ContentRootPath)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(configuration["StorageConnectionString"]);
        });

        services.AddScoped<IBlobContext<BlogMaster>, Context>(serviceProvider =>
        {
            var blobStoreHelper = serviceProvider.GetRequiredService<BlobStoreHelper>();

            var data = JsonSerializer.Deserialize<BlogMaster>(blobStoreHelper.GetBlobAsync().Result);

            var ctx = new Context(blobStoreHelper, data);
            return ctx;
        });

        services.AddTransient<AuthorStore>();
        services.AddTransient<BlogSeriesStore>();
        services.AddTransient<PostStore>();

        services.AddSingleton<BlobStoreHelper>(serviceProvider =>
        {
            var blobServiceClient = serviceProvider.GetRequiredService<BlobServiceClient>();
            string containername = configuration["BlobContainerName"];
            string filename = configuration["BlobFileName"];
            return new BlobStoreHelper(blobServiceClient, containername, filename);
        });
    })
.Build();

BlobInitializer.CreateContainerAndBlob(host.Services);

host.Run();