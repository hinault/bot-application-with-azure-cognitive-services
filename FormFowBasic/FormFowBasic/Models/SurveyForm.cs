using System;
using System.Collections.Generic;
using Microsoft.Bot.Builder.FormFlow;

namespace FormFowBasic.Models
{
    [Serializable]
    public class SurveyForm
    {
       
        public JobOptions Job;
        public ExperienceOptions Experience;
        public PlatformOptions Platform;
        public List<LanguageOptions> Language;
        public WebFrameworkOptions WebFramework;
        public CloudOptions Cloud;

        public static IForm<SurveyForm> BuildForm()
        {
            return new FormBuilder<SurveyForm>()
                    .Message("Merci de prendre quelques minutes pour repondre aux questions de cette enquête.")
                    .Build();
        }

    }


    public enum JobOptions {Developpeur_junior=1, Developpeur_senior, Architecte, Autre};
    public enum ExperienceOptions {Moin_de_5_ans=1, De_5_a_10_ans, Plus_de_10_ans};
    public enum PlatformOptions {Web =1, Mobile, Cloud, Desktop};
    public enum LanguageOptions {Csharp=1, Java, JavaScript, C, Ruby, Python,  Autre};
    public enum WebFrameworkOptions { ASPNET_Core = 1, AngularJS, Lavarel, ReactJS, NodeJS, Autre};
    public enum CloudOptions {Microsoft_Azure=1, Google_Cloud_platform, Amazon_Web_Services, IBM_Cloud, Autre};


}