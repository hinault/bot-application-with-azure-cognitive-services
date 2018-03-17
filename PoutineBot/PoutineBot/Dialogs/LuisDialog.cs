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
            await context.Forward(new QnaDialog(), ResumeAfterQnaDialog, context.Activity, CancellationToken.None);
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
                        order.Size = SizeOptions.Petite;
                        break;
                    case "moyen":
                        order.Size = SizeOptions.Moyenne;
                        break;
                    case "grand":
                    case "senior":
                        order.Size = SizeOptions.Grande;
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


        private async Task ResumeAfterQnaDialog(IDialogContext context, IAwaitable<object> result)
        {
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

                var message = context.MakeMessage();

                message.Text = "Ci-dessous votre reçu!";
                message.Attachments.Add(GetReceiptCard(order));


                await context.PostAsync(message);
            }
            else
            {
                await context.PostAsync("Form returned empty response!");
            }

            context.Wait(MessageReceived);
        }



        private static Attachment GetReceiptCard(OrderForm order)
        {
            var receiptCard = new ReceiptCard
            {
                Title = "Mr/Mme " + order.Name,
                Facts = new List<Fact> { new Fact("Commande No", "xxxxxxx"), new Fact("Méthode de paiement", "Carte") },
              
            };

            var receiptItems = new List<ReceiptItem>();

            int price=0;

            double tax;

            switch (order.Size)
            {
                case SizeOptions.Petite:
                    price =order.Type==TypeOptions.Classique? 5 : 7;
                    break;

                case SizeOptions.Moyenne:
                    price = order.Type == TypeOptions.Classique ? 7 : 9;
                    break;
                case SizeOptions.Grande:
                    price = order.Type == TypeOptions.Classique ? 9 : 11;
                    break;
            }


            receiptItems.Add(new ReceiptItem("Poutine " + order.Type.ToString() + " " + order.Size.ToString(), price: price.ToString()+"$", quantity: "1"));

            if (order.Extras != null )
            { 
               order.Extras.ForEach(x => {
                receiptItems.Add(new ReceiptItem("Extra " + x.ToString(), price: "1$", quantity: "1"));
            });

                price = price + order.Extras.Count();
            }
            tax = price * 0.15;
            receiptCard.Tax = tax.ToString("###.##") + "$";
            receiptCard.Total = (price + tax).ToString("###.##") + "$";
            receiptCard.Items = receiptItems;
            

            return receiptCard.ToAttachment();
        }

    }
}