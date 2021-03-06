using MarkdownBlog.Infra;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cosmos Client
builder.Services.AddSingleton<CosmosClient>(new CosmosClient(
    builder.Configuration.GetSection("CosmosConnection:Endpoint").Value, 
    builder.Configuration.GetSection("CosmosConnection:Key").Value
    ));

var serviceProvider = builder.Services.BuildServiceProvider();

builder.Services.AddCosmosStore("Sample1", serviceProvider.GetService<CosmosClient>());

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
