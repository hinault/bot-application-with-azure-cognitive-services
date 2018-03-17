using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System.Configuration;

namespace SentimentAnalysisBot.Services
{
    public class SentimentAnalysisService
    {

        public static async Task<double> AnalyseSentiment(string message)
        {

            var client = new TextAnalyticsAPI
            {
                AzureRegion = AzureRegions.Westus,
                SubscriptionKey = ConfigurationManager.AppSettings["TextAnalyticsSubscriptionKey"]
            };


            var sentiment = await client.SentimentAsync(new MultiLanguageBatchInput(
                   new[] { new MultiLanguageInput("fr", text: message) }));

            return sentiment.Documents[0].Score.Value;
        }
    }
}