using gov_moderator.Models;
using Microsoft.Azure.Documents.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gov_moderator.Services
{
    public class ServiceFactory
    {
        private GMConfig config;

        public ServiceFactory(GMConfig config)
        {
            this.config = config;
        }

        public CloudStorageAccount CreateCloudStorageAccount() =>
            new CloudStorageAccount(
                new StorageCredentials(
                    accountName: this.config.StorageConfig.AccountName,
                    keyValue: this.config.StorageConfig.AccountKey),
                endpointSuffix: this.config.StorageConfig.EndpointSuffix,
                useHttps: true);

        public CloudBlobClient CreateCloudBlobClient() => this.CreateCloudStorageAccount().CreateCloudBlobClient();

        public CloudQueueClient CreateCloudQueueClient() => this.CreateCloudStorageAccount().CreateCloudQueueClient();

        public DocumentClient CreateDocumentClient() => new DocumentClient(new Uri(this.config.CosmosDbConfig.EndpointUri), this.config.CosmosDbConfig.PrimaryKey);

    }
}
