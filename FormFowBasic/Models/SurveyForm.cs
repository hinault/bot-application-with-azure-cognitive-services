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
        public LanguageOptions Language;
        public DatabaseOptions Database;

        public static IForm<SurveyForm> BuildForm()
        {
            return new FormBuilder<SurveyForm>()
                    .Message("Merci de prendre quelques minutes pour repondre aux questions de cette enquête.")
                    .Build();
        }

    }


    public enum JobOptions {Developpeur_junior=1, Developpeur_senior, Architecte, Autre};
    public enum ExperienceOptions {Moin_de_5_ans=1, De_5_a_10_ans};
    public enum PlatformOptions {Web =1, Mobile, Cloud};
    public enum LanguageOptions {Csharp=1, Java, JavaScript, C, Ruby, Python,  Autre};
    public enum DatabaseOptions {Sql_Server=1, MySQL, Oracle };
   /* public enum WebFrameworkOptions { };
    public enum CloudOptions { };*/
}