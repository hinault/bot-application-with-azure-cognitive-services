using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuisBot.Forms
{
    [Serializable]
    public class OrderForm
    {

        public Boolean SizeSelected = false;
        public Boolean TypeSelected = false;
        [Prompt("Veuillez selectionner votre type de poutine ? {||}")]
        public TypeOptions Type;
        [Prompt("Veuillez choisir la taille ? {||}")]
        public SizeOptions Size;
        [Prompt("Voulez-vous des extras ? {||}")]
        public Boolean AddExtra { get; set; }
        [Prompt("Veuillez choisir les extras ? {||}")]
        public List<ExtraOptions> Extras;
        

        public static IForm<OrderForm> BuildForm()
        {

            return new FormBuilder<OrderForm>()
                    .Message("Merci de prendre quelques minutes pour repondre aux questions de cette enquête.")
                    .Field(nameof(Type), state=>!state.TypeSelected)
                    .Field(nameof(Size), state=>!state.SizeSelected)
                    .Field(nameof(AddExtra))
                    .Field(nameof(Extras), state=>state.AddExtra)
                    .Confirm("Est-ce votre selection ? {*}")
                    .Build();
        }

        private static NextStep SetNextAddExtra(object value, OrderForm state)
        {
            if ((bool)value == true)
            {
                return new NextStep(new[] { nameof(Extras) });
            }
            else
            {

                return new NextStep();
            }
        }


    }


   

    public enum TypeOptions {
        [Describe(title:"Specialité de la maison", description: "Fromage en grains, mozzarella, boeuf braisé et sauce au vin rouge.", 
            image: "http://rdonfack.developpez.com/images/maison.PNG")]
        Maison,
        [Describe(title: "Classique", description: "Fromage en grains, frites et sauce maison",
            image: "http://rdonfack.developpez.com/images/classique.PNG")]
        Classique,
        [Describe(title: "Le fermier", description: "Saucisses italiennes, poivrons rouges, augergines marinées, grains frais, sauce à la viande et mozzarella gratiné.", 
            image: "http://rdonfack.developpez.com/images/fermier.PNG")]
        Fermier,
        [Describe(title: "Le parrain", description: "Poulet grillé, bacon, tomates en dés et fromage en grain.", 
            image: "http://rdonfack.developpez.com/images/parrain.PNG")]
        Parrain
        }
    public enum SizeOptions {Petit, Moyen, Grand};
    public enum ExtraOptions {
        Bacon,
        Viande_haché,
        Jambon,
        Pepperoni,
        Oeuf,
        Porc_effiloché,
        Steak
    }
   
   
}