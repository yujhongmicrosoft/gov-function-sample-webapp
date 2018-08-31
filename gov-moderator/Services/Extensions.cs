using gov_moderator.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gov_moderator.Services
{
    public static class Extensions
    {
        public static async Task<Document> CreateDocumentAsync(this DocumentClient docClient, string docCollection, object document)
        {
            return await docClient.CreateDocumentAsync(docCollection.ToDocCollectionUri(), document);
        }

        public static Uri ToDocCollectionUri(this string dc) => UriFactory.CreateDocumentCollectionUri(DocDbNames.DbName, dc);
        public static Uri ToDocUri(this string dc, string id) => UriFactory.CreateDocumentUri(DocDbNames.DbName, dc, id);


        /// <summary>
        /// This enables us to bind directly to our POCO config object and avoid the IOptions<T> dependency eveywhere
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }
    }
}
