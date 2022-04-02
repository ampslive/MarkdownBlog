using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownBlog.Infra
{
    public class CosmosHelper
    {
        // Replace <documentEndpoint> with the information created earlier
        public static readonly string EndpointUri = "<documentEndpoint>";

        // Set variable to the Primary Key from earlier.
        private static readonly string PrimaryKey = "<your primary key>";

        // The Cosmos client instance
        public CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The names of the database and container we will create
        private string databaseId = "az204Database";
        private string containerId = "az204Container";
    }
}
