using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, CONCERN, ROUTIES, INGREDIENT, SKINTYPE, ALLERGIES, SPF, MASK, EYECONCERN, FINAL
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            //this.oOrder.Phone = sPhone;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome! How can I assist you with your skincare today?");
                    this.nCur = State.CONCERN;
                    break;
                case State.CONCERN:
                    this.oOrder.Assist = sInMessage;
                    aMessages.Add("What is your primary skincare concern: [Acne], [Dryness], or [Anti-aging]?");
                    this.nCur = State.ROUTIES;
                    this.oOrder.Save();
                    break;
                case State.ROUTIES:
                    this.oOrder.Concern = sInMessage;
                    aMessages.Add("Are you looking for [Morning], [Night], or [Both] skincare routines?");
                    this.nCur = State.INGREDIENT;
                    break;
                case State.INGREDIENT:
                    this.oOrder.Routins = sInMessage;
                    aMessages.Add("Any specific ingredient preferences like [Hyaluronic Acid] or [Vitamin C]?");
                    this.nCur = State.SKINTYPE;
                    break;
                case State.SKINTYPE:
                    this.oOrder.Ingredient = sInMessage;
                    aMessages.Add("How would you describe your skin type: [Oily], [Combination], or [Dry]?");
                    this.nCur = State.ALLERGIES;
                    this.oOrder.Save();
                    break;
                case State.ALLERGIES:
                    this.oOrder.SkinType = sInMessage;
                    aMessages.Add("Do you have any skin allergies or sensitivities?");
                    this.nCur = State.SPF;
                    break;
                case State.SPF:
                    this.oOrder.Allergies = sInMessage;
                    aMessages.Add("What level of [SPF] are you comfortable with for your daily routine?");
                    this.nCur = State.MASK;
                    break;
                case State.MASK:
                    this.oOrder.SPF = sInMessage;
                    aMessages.Add("Would you like tips on [DIY Masks] or [Professional Treatments]?");
                    this.nCur = State.EYECONCERN;
                    break;
                case State.EYECONCERN:
                    this.oOrder.Mask = sInMessage;
                    aMessages.Add("Any specific concerns about [Under-eye] or [Uneven Skin Tone]?");
                    this.nCur = State.FINAL;
                    break;

                case State.FINAL:
                    this.oOrder.EyeConcern = sInMessage;
                    aMessages.Add("Your appoinment is booked. Thanks for contacting us.");
                    this.oOrder.Save();
                    break;

                    //case State.SIZE:
                    //    this.oOrder.Size = sInMessage;
                    //    this.oOrder.Save();
                    //    aMessages.Add("What protein would you like on this  " + this.oOrder.Size + " Shawarama?");
                    //    this.nCur = State.PROTEIN;
                    //    break;
                    //case State.PROTEIN:
                    //    string sProtein = sInMessage;
                    //    aMessages.Add("What toppings would you like on this (1. pickles 2. Tzaki) " + this.oOrder.Size + " " + sProtein + " Shawarama?");
                    //    break;



            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

    }
}
