using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.FormFlow;
using PoutineBot.Forms;
using System.Linq;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Threading;
using PoutineBot.Services;

namespace PoutineBot.Dialogs
{
    
    [Serializable]
    public class LuisDialog : LuisDialog<object>
    {
        public LuisDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"],
            ConfigurationManager.AppSettings["LuisAPIKey"],
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        {



        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            //Forward request to QnA Maker
            await context.Forward(new QnaDialog(), ResumeAfterDialog, context.Activity, CancellationToken.None);
        }

        [LuisIntent("Order.Poutine")]
        public async Task OrderIntent(IDialogContext context, LuisResult result)
        {
           
            var typeEntity = result.Entities.FirstOrDefault(x => x.Type == "Type")?.Entity;

            var SizeEntity = result.Entities.FirstOrDefault(x => x.Type == "Taille")?.Entity;

            var orderFormDialog = new OrderFormDialog
            {
                Type = typeEntity,
                Size = SizeEntity

            };
            //Call orderFormDialog as a child dialog
            context.Call(orderFormDialog, ResumeAfterDialog);

        }


        [LuisIntent("Feedback.RecieveFeedback")]
        public async Task RecieveFeedbackIntent(IDialogContext context, LuisResult result)
        {
            await this.ProcessSentimentAnalysis(context, result);
        }

        [LuisIntent("Feedback.GetFeedback")]
        public async Task GetFeedbackIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("Order.Cancel")]
        public async Task CancelIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("Reservation.Reserve")]
        public async Task ReservationIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        private async Task ShowLuisResult(IDialogContext context, LuisResult luisResult)
        {
            await context.PostAsync($"You have reached {luisResult.Intents[0].Intent}. You said: {luisResult.Query}");
            context.Wait(MessageReceived);
        }


        private async Task ResumeAfterDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }


        private async Task ProcessSentimentAnalysis(IDialogContext context, LuisResult luisResult)
        {

            var result = await TextAnalyticsService.AnalyseSentiment(luisResult.Query);
            if (result.Score.Value>0.7)
            {
                await context.PostAsync("Merci d'apprecié nos services. Nous travaillons dur pour vous offrir un service de qualité");
                context.Wait(MessageReceived);
            }
            else
            {
                var feedbackForm = new FormDialog<FeedbackForm>(new FeedbackForm(), FeedbackForm.BuildForm, FormOptions.PromptInStart);
                context.Call<FeedbackForm>(feedbackForm, ResumeAfterDialog);

            }

        }

    }
}