using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis.Models;
using PoutineBot.Forms;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Builder.FormFlow;

namespace PoutineBot.Dialogs
{
    [Serializable]
    public class OrderFormDialog : IDialog<object>
    {
        public string Type { get; set; }
        public string Size { get; set; }

        public async Task StartAsync(IDialogContext context)
        {

            await context.PostAsync("Bienvenue au service de commande. Notre assistant vous guidera.");

            var order = new OrderForm();

           
                switch (Type)
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
           
            
                switch (Size)
                {
                    case "petit":
                    case "junior":
                    case "petite":
                        order.Size = SizeOptions.Petite;
                        break;
                    case "moyen":
                    case "moyenne":
                        order.Size = SizeOptions.Moyenne;
                        break;
                    case "grand":
                    case "grande":
                    case "senior":
                        order.Size = SizeOptions.Grande;
                        break;

                }
           

          
            var orderForm =  new FormDialog<OrderForm>(order, OrderForm.BuildForm, FormOptions.PromptInStart);
           
            context.Call<OrderForm>(orderForm, OrderFormComplete);

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
                await context.PostAsync("Opération annulée!");
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
                await context.PostAsync("Opération annulée!");
            }
            context.Done(true);
        }


        private static Attachment GetReceiptCard(OrderForm order)
        {
            var receiptCard = new ReceiptCard
            {
                Title = "Mr/Mme " + order.Name,
                Facts = new List<Fact> { new Fact("Commande No", "xxxxxxx"), new Fact("Méthode de paiement", "Carte") },

            };

            var receiptItems = new List<ReceiptItem>();

            int price = 0;

            double tax;

            switch (order.Size)
            {
                case SizeOptions.Petite:
                    price = order.Type == TypeOptions.Classique ? 5 : 7;
                    break;

                case SizeOptions.Moyenne:
                    price = order.Type == TypeOptions.Classique ? 7 : 9;
                    break;
                case SizeOptions.Grande:
                    price = order.Type == TypeOptions.Classique ? 9 : 11;
                    break;
            }


            receiptItems.Add(new ReceiptItem("Poutine " + order.Type.ToString() + " " + order.Size.ToString(), price: price.ToString() + "$", quantity: "1"));

            if (order.Extras != null)
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