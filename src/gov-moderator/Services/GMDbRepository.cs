using gov_moderator.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gov_moderator.Services
{
    public class GMDbRepository 
    {
        private DocumentClient docClient;

        public GMDbRepository(DocumentClient docClient)
        {
            this.docClient = docClient;
        }

        public async Task InitializeDatabase()
        {
            await this.CreateCollectionIfNotExistsAsync(DocDbNames.Images);
        }

        public async Task<Document> CreateImageDoc<T>(T doc)
        {
            return await this.docClient.CreateDocumentAsync(DocDbNames.Images, doc);
        }

        #region Private Methods

        private async Task CreateCollectionIfNotExistsAsync(string collectionName)
        {
            try
            {
                await this.docClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DocDbNames.DbName, collectionName));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await this.docClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DocDbNames.DbName),
                        new DocumentCollection { Id = collectionName },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }

        #endregion
    }
}
