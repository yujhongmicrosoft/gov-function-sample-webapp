using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gov_moderator.Models
{
    public class ImageFile
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        //[JsonProperty("name")]
        //public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("blobUri")]
        public string BlobUri { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }

    public class ImageFileModel
    {
        //public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile uploadFile { get; set; }

    }
}
