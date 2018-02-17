using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using FormFowBasic.Models;
using Microsoft.Bot.Builder.FormFlow;
using System;

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
            else if (activity.Type == ActivityTypes.ContactRelationUpdate)
            {
                ConnectorClient client = new ConnectorClient(new Uri(activity.ServiceUrl));

                var reply = activity.CreateReply();

                reply.Text = "Bonjour";

                await client.Conversations.ReplyToActivityAsync(reply);

            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
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