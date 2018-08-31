using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gov_moderator.Models
{
    public static class Constants
    {
        public static string OcpSubscriptionKey = "Ocp-Apim-Subscription-Key";
        public static string PredictionKey = "Prediction-Key";
    }

    internal static class DocDbNames
    {
        public const string DbName = "gov-moderator";
        public const string Images = "images";
    }
}
