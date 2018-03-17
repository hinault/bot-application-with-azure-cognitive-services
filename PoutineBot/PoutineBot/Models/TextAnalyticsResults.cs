using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoutineBot.Models
{
    public class TextAnalyticsResults
    {

        public double? Score {get; set;}

        public string LanguageName { get; set; }

        public string LanguageIso6391Name { get; set; }

        public IList<string> KeyPhrases { get; set; }


    }
}