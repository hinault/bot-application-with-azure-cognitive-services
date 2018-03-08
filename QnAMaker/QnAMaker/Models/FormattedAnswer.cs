using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QnAMaker.Models
{
    public class FormattedAnswer
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string BoutonText { get; set; }
    }
}