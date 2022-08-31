using Azure;
using MarkdownBlog.Domain.Contracts;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace MarkdownBlog.Infra;

public class CosmosHelper : IDatabaseHelper
{
    private readonly CosmosClient client;
    private readonly Database db;

    public CosmosHelper(CosmosClient cosmosClient, CosmosConfig cosmosConfig)
    {
        client = cosmosClient;
        db = cosmosClient.GetDatabase(cosmosConfig.DatabaseName);
    }

    public async Task<T> CreateAsync<T>(T obj, string containerName, string partitionKey)
    {
        var container = db.GetContainer(containerName);
        return await container.UpsertItemAsync(obj, new PartitionKey(partitionKey));
    }

    public async Task<T?> GetAsync<T>(string id, string containerName, string partitionKey)
    {
        var container = db.GetContainer(containerName);
        ItemResponse<T> response = await container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
        
        if(response.StatusCode == HttpStatusCode.NotFound)
            return default;
            
        return response.Resource;
    }

    public async Task<List<T>> QueryAsync<T>(string inputQuery, string containerName, string[] parameters)
    {
        List<T> result = new();

        QueryDefinition query = new QueryDefinition(inputQuery);

        for (int i = 0; i <= parameters.Length - 1; i++)
            query.WithParameter($"@p{i}", parameters[i]);

        var container = db.GetContainer(containerName);

        using (FeedIterator<T> feedIterator = container.GetItemQueryIterator<T>(query, null, new QueryRequestOptions()))
        {
            while (feedIterator.HasMoreResults)
            {
                foreach (T item in await feedIterator.ReadNextAsync())
                    result.Add(item);
            }
        }

        return result;
    }
}
