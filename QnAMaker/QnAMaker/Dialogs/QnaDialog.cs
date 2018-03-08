using System;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace QnAMaker.Dialogs
{
    [Serializable]
    public class QnaDialog : QnAMakerDialog
    {
        public QnaDialog() : base(new QnAMakerService(new QnAMakerAttribute(ConfigurationManager.AppSettings["QnaSubscriptionKey"],
            ConfigurationManager.AppSettings["QnaKnowledgebaseId"], "Désolé, je n'ai pas trouvé une reponse à cette question", 0.5)))
        { }

        protected override async Task RespondFromQnAMakerResultAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {
            // answer is a string
            var answer = result.Answers.First().Answer;

         

            Activity reply = ((Activity)context.Activity).CreateReply();

            try { 
            var response = JObject.Parse(answer);
            }
            catch(JsonReaderException)
            { 
            reply.Text = answer;
            }
            await context.PostAsync(reply);

        }
    }
}