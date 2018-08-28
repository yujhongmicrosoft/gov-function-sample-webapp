using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ProjectOxford.Vision;

namespace ModeratorFunctionApp
{
    public static class AnalyzeImage
    {
        [FunctionName("AnalyzeImage")]
        public static async Task Run(
            [QueueTrigger("image-queue", Connection = "storage-connection")]ImageQueueItem imageQueueItem, 
            [Blob("images/{BlobName}", FileAccess.Read, Connection = "storage-connection")] Stream image,
            [DocumentDB("gov-moderator", "images", Id = "{DocumentId}", ConnectionStringSetting = "GovModeratorDb")] dynamic inputDocument,
            TraceWriter log)
        {
            (bool containsCar, string caption) = await PassesImageModerationAsync(image); // use Vision API
            inputDocument.status = containsCar ? "Approved" : "Rejected";
        }

        private static async Task<(bool, string)> PassesImageModerationAsync(Stream image)
        {
            var client = CreateVisionClient();
            var result = await client.AnalyzeImageAsync(image, VisualFeatures);
            bool containsCar = result.Description.Tags.Contains("car");
            string caption = result?.Description?.Captions.FirstOrDefault()?.Text;
            return (containsCar, caption);
        }

        private static VisionServiceClient CreateVisionClient() => new VisionServiceClient(Environment.GetEnvironmentVariable("VisionApiKey"), "https://cognitivevirginiaprod.azure-api.us/vision/v1.0");

        private static readonly VisualFeature[] VisualFeatures = new[] { VisualFeature.Description, VisualFeature.Tags };
    }
}
