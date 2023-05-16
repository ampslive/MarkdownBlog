using Azure.Storage.Blobs;
using MarkdownBlog.API.Endpoints;
using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Models;
using MarkdownBlog.Domain.Store;
using MarkdownBlog.Infra;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Microsoft.Extensions.Azure;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration.GetSection("StorageConnectionString"));
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

builder.Services.AddSingleton<BlobStoreHelper>(serviceProvider =>
{
    var blobServiceClient = serviceProvider.GetRequiredService<BlobServiceClient>();
    string containername = builder.Configuration.GetValue<string>("BlobContainerName");
    string filename = builder.Configuration.GetValue<string>("BlobFileName");
    return new BlobStoreHelper(blobServiceClient, containername, filename);
});

var app = builder.Build();

BlobInitializer.CreateContainerAndBlob(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapBlogSeriesEndpoints();
app.MapAuthorEndpoints();
app.Run();
