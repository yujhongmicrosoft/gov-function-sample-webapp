using gov_moderator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gov_moderator.Services
{
    public class ImageManager
    {
        //private GMDbRepository repository;
        private CloudQueueClient queueClient;
        private IStorageClient storageClient;
        private DocumentClient docClient;

        public ImageManager(IStorageClient storageClient, DocumentClient docClient, CloudQueueClient queueClient)//, GMDbRepository repository)
        {
            this.storageClient = storageClient;
            this.docClient = docClient;
            //this.repository = repository;
            this.queueClient = queueClient;
        }

        public async Task Initialize()
        {
            await this.docClient.CreateDatabaseIfNotExistsAsync(new Database { Id = DocDbNames.DbName });
            await this.docClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DocDbNames.DbName), new DocumentCollection { Id = DocDbNames.Images });

        }
        public async Task<List<ImageFile>> GetImages()
        {
            IDocumentQuery<ImageFile> query = this.docClient.CreateDocumentQuery<ImageFile>(DocDbNames.Images.ToDocCollectionUri())
                .OrderBy(x => x.Created)
                .AsDocumentQuery();
            return await ExecuteFullQuery<ImageFile>(query);
        }

        public async Task<ImageFile> GetImage(string id)
        {
            var img = await this.docClient.ReadDocumentAsync<ImageFile>(DocDbNames.Images.ToDocUri(id));
            return img;
        }

        public async Task DeleteImage(string id)
        {
            await this.storageClient.DeleteBlob(id);
            await this.docClient.DeleteDocumentAsync(DocDbNames.Images.ToDocUri(id));
        }

        public async Task<string> UploadNewImageFile(ImageFileModel file, IFormFile uploadFile)
        {
            var recordId = Guid.NewGuid().ToString();

            // 1. Save Image
            var blobUri = await this.storageClient.AddNewBlob(recordId, file.uploadFile);

            // 2. Save doc
            await this.docClient.CreateDocumentAsync(
                DocDbNames.Images.ToDocCollectionUri(), 
                new ImageFile
                {
                    Id = recordId.ToString(),
                    Description = file.Description,
                    BlobUri = blobUri,
                    Status = "Pending",
                    Created = DateTime.UtcNow
                });

            // 3. Queue message
            var queueMsg = new { BlobName = recordId, DocumentId = recordId };
            var queue = this.queueClient.GetQueueReference("image-queue");
            await queue.CreateIfNotExistsAsync();
            await queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(queueMsg)));

            return recordId;
        }

        #region Private Methods

        private static async Task<List<T>> ExecuteFullQuery<T>(IDocumentQuery<T> query)
        {
            var results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }
            return results;
        }

        #endregion
    }
}
