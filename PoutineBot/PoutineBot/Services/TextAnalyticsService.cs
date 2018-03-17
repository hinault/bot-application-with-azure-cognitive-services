using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System.Configuration;
using System.Collections.Generic;
using PoutineBot.Models;

namespace PoutineBot.Services
{
    public class TextAnalyticsService
    {

        public static async Task<TextAnalyticsResults> AnalyseSentiment(string input)
        {

            var client = new TextAnalyticsAPI
            {
                AzureRegion = AzureRegions.Westus,
                SubscriptionKey = ConfigurationManager.AppSettings["TextAnalyticsSubscriptionKey"]
            };


        
            var language = client.DetectLanguage(
                   new BatchInput(
                       new List<Input>()
                       {
                          new Input("1", input)
                       }));

            var result = new TextAnalyticsResults
            {
                 LanguageName = language.Documents[0].DetectedLanguages[0].Name,
                 LanguageIso6391Name = language.Documents[0].DetectedLanguages[0].Iso6391Name
            };


            var keyPhrases = await client.KeyPhrasesAsync(new MultiLanguageBatchInput(
                  new[] { new MultiLanguageInput(language:result.LanguageIso6391Name, id: "1", text: input) }));

            result.KeyPhrases = keyPhrases.Documents[0].KeyPhrases;

            var sentiment = await client.SentimentAsync(new MultiLanguageBatchInput(
                   new[] { new MultiLanguageInput(language:result.LanguageIso6391Name, id:"1", text: input) }));

            result.Score = sentiment.Documents[0].Score;
            


            return result;
        }
    }
}