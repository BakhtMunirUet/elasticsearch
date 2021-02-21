using System;
using Elasticsearch.Net;
using Nest;

namespace API.Extensions
{
    public static class ClientExtensions
    {
        public static ElasticClient IndexName(string indexName)
        {
            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var settings = new ConnectionSettings(pool, new HttpConnection())
                .DefaultIndex(indexName);
            ElasticClient client = new ElasticClient(settings);
            // services.AddSingleton(client);

            return client;
        }

    }
}