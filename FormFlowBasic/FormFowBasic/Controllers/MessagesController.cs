using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using FormFowBasic.Models;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Linq;

namespace FormFowBasic
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>


        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, MakeRootDialog);
            }
            
            else
            {
               await HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private async Task HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                if (message.MembersAdded.Any(o => o.Id == message.Recipient.Id))
                {
                    var reply = message.CreateReply("Bonjour");

                    ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));

                    await connector.Conversations.ReplyToActivityAsync(reply);
                }

            }

            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
               
            }
    
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

           
        }

        internal static IDialog<SurveyForm> MakeRootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(SurveyForm.BuildForm))
                .Do(async (context, survey) =>
                {
                    try
                    {
                        var completed = await survey;
                       await context.PostAsync("Merci pour votre participation !");
                    }
                    catch (FormCanceledException<SurveyForm> e)
                    {
                        string reply;
                        if (e.InnerException == null)
                        {
                            reply = "Vous n’avez pas complété l’enquête !";
                        }
                        else
                        {
                            reply = "Erreur. Essayez plus tard!.";
                        }
                        await context.PostAsync(reply);
                    }
                });
        }
    }
}