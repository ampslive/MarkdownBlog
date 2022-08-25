using MarkdownBlog.Domain.Contracts;
using Microsoft.Azure.Cosmos;

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

    public async Task<T> GetAsync<T>(string id, string containerName, string partitionKey)
    {
        var container = db.GetContainer(containerName);
        ItemResponse<T> response = await container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));

        return response.Resource;
    }

    public async void QueryAsync<T>()
    {
        QueryDefinition query = new QueryDefinition(
    "select * from sales s where s.AccountNumber = @AccountInput ")
    .WithParameter("@AccountInput", "Account1");

        var container = db.GetContainer("testcontainer");
        FeedIterator<T> resultSet = container.GetItemQueryIterator<T>(
            query,
            requestOptions: new QueryRequestOptions()
            {
                PartitionKey = new PartitionKey("Account1"),
                MaxItemCount = 1
            });
    }
}
