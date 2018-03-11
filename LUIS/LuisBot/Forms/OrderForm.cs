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
        [Prompt("Veuillez saisir votre nom ? {||}")]
        public string Name { get; set; }


        public static IForm<OrderForm> BuildForm()
        {

            return new FormBuilder<OrderForm>()
                    .Field(nameof(Type))
                    .Field(nameof(Size))
                    .Field(nameof(AddExtra))
                    .Field(nameof(Extras), state=>state.AddExtra)
                    .Field(nameof(Extras), state => state.AddExtra)
                    .Field(nameof(Name))
                    .Confirm("Vos choix sont-ils corrects ? {*}")
                    .Build();
        }

        private static NextStep SetNextAfterAddExtra(object value, OrderForm state)
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
        [Describe(title:"Specialité de la maison", subTitle: "Fromage en grains, mozzarella, boeuf braisé et sauce au vin rouge.", 
            image: "http://rdonfack.developpez.com/images/maison.PNG",message:"Maison")]
        Maison=1,
        [Describe(title: "Classique", subTitle: "Fromage en grains, frites et sauce maison",
            image: "http://rdonfack.developpez.com/images/classique.PNG", message: "Classique")]
        Classique,
        [Describe(title: "Le fermier", subTitle: "Saucisses italiennes, poivrons rouges, augergines marinées, grains frais, sauce à la viande et mozzarella gratiné.", 
            image: "http://rdonfack.developpez.com/images/fermier.PNG", message: "Fermier")]
        Fermier,
        [Describe(title: "Le parrain", subTitle: "Poulet grillé, bacon, tomates en dés et fromage en grain.", 
            image: "http://rdonfack.developpez.com/images/parrain.PNG", message: "Parrain")]
        Parrain
        }
    public enum SizeOptions {
        Petite =1,
        [Terms("Moyen", "Moyenne")]
        Moyenne,
        Grande };
    public enum ExtraOptions {
        Bacon=1,
        Viande_haché,
        Jambon,
        Pepperoni,
        Oeuf,
        Porc_effiloché,
        Steak
    }
   
   
}