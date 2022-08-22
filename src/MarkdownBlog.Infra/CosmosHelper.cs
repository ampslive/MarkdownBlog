using MarkdownBlog.Domain.Contracts;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

    public async Task<T> CreateAsync<T>(T obj, string containerName)
    {
        var container = db.GetContainer(containerName);
        return await container.CreateItemAsync(obj);
    }

    public async void GetAsync<T>()
    {
        string id = "[id]";
        string accountNumber = "[partition-key]";
        var container = db.GetContainer("testcontainer");
        //ItemResponse<T> response = await container.ReadItemAsync(id, new PartitionKey(accountNumber));
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
