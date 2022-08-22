using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownBlog.Infra
{
    public class CosmosConfig
    {
        // Replace <documentEndpoint> with the information created earlier
        public string EndpointUri = "<documentEndpoint>";

        // The database we will create
        public string DatabaseName;

        public CosmosConfig(string endpointUri, string dbName)
        {
            EndpointUri = endpointUri;
            DatabaseName = dbName;
        }
    }
}
