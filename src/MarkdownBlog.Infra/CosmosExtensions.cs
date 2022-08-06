using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace MarkdownBlog.Infra;

//https://docs.microsoft.com/en-us/learn/modules/use-azure-cosmos-db-sql-api-sdk/4-connect-to-online-account

/*
// insert data

// read data

/*
    - QueryDefinition query = new ("SELECT * FROM products p");
    - Define a C# type to represent the type of item you will query
    - await foreach (Product product in container.GetItemQueryIterator<Product>(query))
        {
            Console.WriteLine($"[{product.id}]\t{product.name,35}\t{product.price,15:C}");
        }
    - Pagination
        - Set QueryRequestOptions
            QueryRequestOptions options = new()
            {
                MaxItemCount = 100
            };
        - Use FeedIterator
            FeedIterator<Product> iterator = container.GetItemQueryIterator<Product>(query, requestOptions: options);
        - Use HasMoreResults...
            while(iterator.HasMoreResults)
            {
                foreach(Product product in await iterator.ReadNextAsync())
                {
                    // Handle individual items
                }
            }
 */

public static class CosmosExtensions
{
    public static IServiceCollection CreateDB(this IServiceCollection services, string dbName, CosmosClient cosmosClient)
    {
        // register your services here
        cosmosClient.CreateDatabaseIfNotExistsAsync(dbName).Wait();
        return services;
    }

    public static IServiceCollection CreateContainer(this IServiceCollection services, string dbName, Dictionary<String, String> containerCollection, CosmosClient cosmosClient)
    {
        var db = cosmosClient.GetDatabase(dbName);

        foreach (var t in containerCollection)
            db.CreateContainerIfNotExistsAsync(t.Key, $"/{t.Value}").Wait();

        return services;
    }
}
