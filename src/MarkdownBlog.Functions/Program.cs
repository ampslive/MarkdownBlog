using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using MarkdownBlog.Domain.Models;
using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Infra;
using System.Text.Json;
using Azure.Storage.Blobs;
using MarkdownBlog.Domain.Store;
using Microsoft.Extensions.Configuration;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureHostConfiguration(config =>
    {
        config.AddJsonFile("local.settings.json", true, true);
    })
    .ConfigureServices((ctx,services) =>
    {
        var configuration = ctx.Configuration;

        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(configuration["StorageConnectionString"]);
        });

        services.AddScoped<IBlobContext<BlogMaster>, Context>(serviceProvider =>
        {
            var blobStoreHelper = serviceProvider.GetRequiredService<BlobStoreHelper>();

            var data = JsonSerializer.Deserialize<BlogMaster>(blobStoreHelper.GetBlobAsync().Result);

            var context = new Context(blobStoreHelper, data);
            return context;
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


        services.AddScoped(serviceProvider =>
        {
            BlobInitializer.CreateContainerAndBlob(serviceProvider);
            return serviceProvider.ToString();
        });

        services.Configure<JsonSerializerOptions>(options =>
        {
            options.PropertyNameCaseInsensitive = true;
        });

    })
    .Build();

host.Run();
