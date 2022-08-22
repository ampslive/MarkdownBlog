using MarkdownBlog.Domain.Contracts;
using MarkdownBlog.Domain.Repositories;
using MarkdownBlog.Domain.Store;
using MarkdownBlog.Infra;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CosmosConfig>(new CosmosConfig(
        builder.Configuration.GetSection("CosmosConnection:Endpoint").Value,
        builder.Configuration.GetSection("CosmosConnection:DbName").Value
    ));

//Cosmos Client
builder.Services.AddSingleton<CosmosClient>(new CosmosClient(
    builder.Configuration.GetSection("CosmosConnection:Endpoint").Value, 
    builder.Configuration.GetSection("CosmosConnection:Key").Value,
    new CosmosClientOptions()
    {
        SerializerOptions = new CosmosSerializationOptions()
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
    }
    ));

builder.Services.AddTransient<IDatabaseHelper, CosmosHelper>();

//repos
builder.Services.AddTransient<BlogRepo>();

//store
builder.Services.AddTransient<BlogStore>();

var serviceProvider = builder.Services.BuildServiceProvider();

var containerList = new Dictionary<string, string> { 
    { "Blogs", "id" }, 
};

var dbName = builder.Configuration.GetSection("CosmosConnection:DbName").Value;

builder.Services.CreateDB(dbName, serviceProvider.GetService<CosmosClient>())
                .CreateContainer(dbName, containerList, serviceProvider.GetService<CosmosClient>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
