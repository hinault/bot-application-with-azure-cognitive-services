using System;
using System.Configuration;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.FormFlow;
using LuisBot.Forms;
using System.Linq;

namespace LuisBot.Dialogs
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

        //private readonly BuildFormDelegate<OrderForm> MakeOrderForm;

        //internal OrderFormDialog(BuildFormDelegate<OrderForm> makeOrderForm)
        //{
        //    MakeOrderForm = makeOrderForm;
        //}

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("Order.Poutine")]
        public async Task OrderIntent(IDialogContext context, LuisResult result)
        {

            var order = new OrderForm();
           
            var typeEntity = result.Entities.FirstOrDefault(x=>x.Type == "Type");

            if (typeEntity != null)
             {
                order.TypeSelected = true;
                switch (typeEntity.Entity)
                {
                    case "classique":
                    case "simple":
                    case "regulier":
                    case "régulier":
                    case "regulière":
                    case "régulière":
                        order.Type = TypeOptions.Classique;
                        break;
                    case "fermier":
                        order.Type = TypeOptions.Fermier;
                        break;
                    case "maison":
                        order.Type = TypeOptions.Maison;
                        break;
                    case "parrain":
                        order.Type = TypeOptions.Parrain;
                        break;
                }
            }

            var SizeEntity = result.Entities.FirstOrDefault(x => x.Type == "Taille");

            if(SizeEntity!=null)
            {
                order.SizeSelected = true;

                switch (SizeEntity.Entity)
                {
                    case "petit":
                    case "junior":
                        order.Size = SizeOptions.Petit;
                        break;
                    case "moyen":
                        order.Size = SizeOptions.Moyen;
                        break;
                    case "grand":
                    case "senior":
                        order.Size = SizeOptions.Grand;
                        break;

                }   
            }

            var orderForm = new FormDialog<OrderForm>(order, OrderForm.BuildForm, FormOptions.PromptInStart);
            context.Call<OrderForm>(orderForm, OrderFormComplete);

           // await this.ShowLuisResult(context, result);
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

        [LuisIntent("Feedback.RecieveFeedback")]
        public async Task RecieveFeedbackIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("Feedback.GetFeedback")]
        public async Task GetFeedbackIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

      
        private async Task ShowLuisResult(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}");
            context.Wait(MessageReceived);
        }



        private async Task OrderFormComplete(IDialogContext context, IAwaitable<OrderForm> result)
        {
            OrderForm order = null;
            try
            {
                order = await result;
            }
            catch (OperationCanceledException)
            {
                await context.PostAsync("You canceled the form!");
                return;
            }

            if (order != null)
            {
                await context.PostAsync("Your Pizza Order: " + order.ToString());
            }
            else
            {
                await context.PostAsync("Form returned empty response!");
            }

            context.Wait(MessageReceived);
        }

    }
}