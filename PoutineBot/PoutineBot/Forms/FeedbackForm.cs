using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using System;
using System.Collections.Generic;

namespace PoutineBot.Forms
{
    [Serializable]
    public class FeedbackForm
    {

        [Prompt("Nous sommes désolés que vous n'ayez pas apprécié. Voulez-vous nous laisser votre contact afin que nous puissions vous contacter ? {||}")]
        public Boolean LeaveFeedback { get; set; }
        [Prompt("Veuillez saisir votre nom ? {||}")]
        public string Name { get; set; }
        [Prompt("Veuillez saisir votre numéro de téléphone ? {||}")]
        [Pattern(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        public string PhoneNumber { get; set; }
     

        public static IForm<FeedbackForm> BuildForm()
        {

            return new FormBuilder<FeedbackForm>()
                    .Field(nameof(LeaveFeedback))
                    .Field(nameof(Name), state=>state.LeaveFeedback)
                    .Field(nameof(PhoneNumber), state => state.LeaveFeedback)
                    .Message("Merci. Un de nos responsable vous contactera sous peu.", condition: (form) => form.LeaveFeedback)
                     .Message("Merci.", condition: (form) => !form.LeaveFeedback)
                    .Build();
        }

    }
}