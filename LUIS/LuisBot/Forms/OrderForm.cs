using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuisBot.Forms
{
    [Serializable]
    public class OrderForm
    {

        public Boolean Lenghtactf { get; set; }
        [Prompt("Quel est votre poste ? {||}")]
        public JobOptions Job;
        [Prompt("Combien d'années d'expérience avez-vous ? {||}")]
        public ExperienceOptions Experience;
        [Prompt("Pour quelle plateforme développez-vous ? {||}")]
        public PlatformOptions Platform;
        

        public static IForm<OrderForm> BuildForm()
        {
            return new FormBuilder<OrderForm>()
                    .Message("Merci de prendre quelques minutes pour repondre aux questions de cette enquête.")
                    .Build();
        }

    }


    public enum JobOptions { Developpeur_junior = 1, Developpeur_senior, Architecte, Autre };
    public enum ExperienceOptions { Moins_de_5_ans = 1, De_5_a_10_ans, Plus_de_10_ans };
    public enum PlatformOptions { Web = 1, Mobile, Cloud, Desktop };

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
    public 
   
   
}