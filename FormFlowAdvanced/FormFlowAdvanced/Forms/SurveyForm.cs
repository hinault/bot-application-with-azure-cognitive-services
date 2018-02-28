using System;
using System.Collections.Generic;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;

namespace FormFowBasic.Forms
{
    [Serializable]
    public class SurveyForm
    {
      
        [Prompt("Quel est votre poste ? {||}")]
        public JobOptions Job;
        [Prompt("Combien d'années d'expérience avez-vous ? {||}")]
        public ExperienceOptions Experience;
        [Prompt("Pour quelle plateforme developpez-vous ? {||}")]
        public PlatformOptions Platform;
        [Prompt("Quels langages de programmation utilisez-vous ? {||}")]
        public List<LanguageOptions> Language;
        [Prompt("Quel Framework Web utilisez-vous ? {||}")]
        public WebFrameworkOptions WebFramework;
        [Prompt("Quel outil de développement mobile utilisez-vous ? {||}")]
        public MobileDevToolsOptions MobileDevTools;
        [Prompt("Quelle plateforme Cloud utilisez-vous ? {||}")]
        public CloudOptions Cloud;
        [Prompt("Voulez-vous recevoir notre newsletter ? {||}")]
        public bool Newsletter;
        [Prompt("Quel est votre adresse email ? {||}")]
        public string Email;


        public static IForm<SurveyForm> BuildForm()
        {
            return new FormBuilder<SurveyForm>()
                    .Message("Merci de prendre quelques minutes pour repondre aux questions de cette enquête.")
                    .Field(nameof(Job))
                    .Field(nameof(Experience))
                    .Field(nameof(Language))
                    .Field(new FieldReflector<SurveyForm>(nameof(Platform))
                        .SetNext(SetNextAfterPlatform))
                    .Field(nameof(WebFramework), state=>state.Platform==PlatformOptions.Web)
                    .Field(nameof(MobileDevTools), state => state.Platform == PlatformOptions.Mobile)
                    .Field(nameof(Cloud), state => state.Platform == PlatformOptions.Cloud)
                     .Field(new FieldReflector<SurveyForm>(nameof(Newsletter))
                        .SetNext(SetNextAfterNewsletter))
                     .Field(nameof(Email), state => state.Newsletter)
                     .Confirm("Est-ce votre selection ? {*}")
                    .Build();
                      
        }

        private static NextStep SetNextAfterPlatform(object value, SurveyForm state)
        {
            var selection = (PlatformOptions)value;
            state.Platform = selection;
            if (selection == PlatformOptions.Web)
            {
                return new NextStep(new[] { nameof(WebFramework) });
            }
            else if (selection == PlatformOptions.Mobile)
            {
                return new NextStep(new[] { nameof(MobileDevTools) });
            }
            else
            {
                return new NextStep(new[] { nameof(Cloud) });
            }
        }


        private static NextStep SetNextAfterNewsletter(object value, SurveyForm state)
        {
            if ((bool)value == true)
            {
                return new NextStep(new[] { nameof(Email) });
            }
            else
            {

                return new NextStep();
            }
        }

    }


        public enum JobOptions
    {
        [Describe("Développeur junior")]
        [Terms("junior", "Développeur junior", "Developpeur junior")]
        Developpeur_junior =1,
        [Describe("Développeur sénior")]
        [Terms("senior", "sénior", "Développeur sénior", "Developpeur senior")]
        Developpeur_senior,
        Architecte,
        Autre
    };

    public enum ExperienceOptions
    {
        Moins_de_5_ans =1,
        De_5_a_10_ans,
        Plus_de_10_ans
    };

    public enum PlatformOptions
    {
        Web =1,
        Mobile,
        Cloud
    };

    public enum LanguageOptions
    {
        [Describe("C#")]
        Csharp =1,
        Java,
        [Describe("JavaScript")]
        JavaScript,
        C,
        Ruby,
        Python,
        Autre
    };

    public enum WebFrameworkOptions
    {
        [Describe("ASP.NET Core")]
        [Terms("ASP.NET Core", "ASP.NET", "ASP")]
        ASPNET_Core = 1,
        [Describe("AngularJS")]
        [Terms("Angular", "AngularJS")]
        AngularJS,
        Lavarel,
        [Describe("ReactJS")]
        [Terms("React","ReactJS")]
        ReactJS,
        [Describe("NodeJS")]
        [Terms("Node","NodeJS")]
        NodeJS,
        Autre
    };

    public enum MobileDevToolsOptions
    {
        Xamarin=1,
        AndroidStudio,
        PhoneGap,
        Appcelerator,
        Sencha,
        Autre
    }

    public enum CloudOptions
    {
        Microsoft_Azure =1,
        Google_Cloud_platform,
        Amazon_Web_Services,
        IBM_Cloud,
        Autre
    };


}
