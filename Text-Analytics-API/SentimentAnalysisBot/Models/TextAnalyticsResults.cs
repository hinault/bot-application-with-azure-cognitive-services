using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SentimentAnalysisBot.Models
{
    public class TextAnalyticsResults
    {

        public double? Score {get; set;}

        public string LanguageName { get; set; }

        public string LanguageIso6391Name { get; set; }

        public IList<string> KeyPhrases { get; set; }

    }
}