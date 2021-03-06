﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using SentimentAnalysisBot.Services;
using System;
using System.Linq;

namespace SentimentAnalysisBot
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
                var result = await TextAnalyticsService.AnalyseSentiment(activity.Text);

                var reply = activity.CreateReply();

                reply.Text = "Votre langue est : " + result.LanguageName + "\n\n";
                reply.Text += "Les mots clés trouvés sont : ";

                foreach (var item in result.KeyPhrases)
                {
                    reply.Text += item + " ";
                }

                if (result.Score.Value > 0.5)
                    reply.Text += "\n\n Vous semblez heureux. Votre score est de : " + result.Score.Value;
                else
                    reply.Text += "\n\n Vous ne semblez pas heureux. Votre score est de : " + result.Score.Value;

                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                await connector.Conversations.ReplyToActivityAsync(reply);
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
                    var reply = message.CreateReply("Bonjour \n\n");
                    reply.Text += "Dites quelque chose afin que nous puissions procéder à son analyse.";

                    ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));

                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }
        }
    }
}