using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gov_moderator.Models
{
    public class GMConfig
    {
        public StorageConfig StorageConfig { get; set; }
        public CosmosDbConfig CosmosDbConfig { get; set; }

    }

    public class StorageConfig
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string EndpointSuffix { get; set; }
    }

    public class CosmosDbConfig
    {
        public string EndpointUri { get; set; }
        public string PrimaryKey { get; set; }
    }
}
