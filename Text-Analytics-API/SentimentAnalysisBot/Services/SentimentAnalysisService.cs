using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;

namespace SentimentAnalysisBot.Services
{
    public class SentimentAnalysisService
    {

        public static async Task<float> AnalyseSentiment(string message)
        {

            ITextAnalyticsAPI client = new TextAnalyticsAPI();



        }
    }
}