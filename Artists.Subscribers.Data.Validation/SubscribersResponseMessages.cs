namespace Artists.Subscribers.Data.Validation
{
    public class SubscribersResponseMessages
    {

        #region Properties

        public string Ex { get; set; }

        public string SubscriberName { get; set; }

        public string SubscriberMail { get; set; }

        #endregion

        #region Constructor  
        
        public SubscribersResponseMessages(string subscriberName, string subscriberMail)
        {
            this.SubscriberName = subscriberName;
            this.SubscriberMail = subscriberMail;
        }

        public SubscribersResponseMessages() { }

        #endregion

        #region Metods
        public string ResponseCreated() => $"Welcome {SubscriberName} to oure subscribers";

        public string ResponseDeleted() => "You are not longer subscribed";

        public string ErrorResponseIsRegistrated() => $"The email address: {SubscriberMail} is alredy registrated";

        public string ErrorResponseNotValidNameAndEmail() => $"The name: {SubscriberName}, and email address: {SubscriberMail}, you provided are not valid";

        public string ErrorResponseNotValidEmail() => $"The mail address: {SubscriberMail} you provided, is not a valid email address";

        public string ErrorResponseNotValidName() => $"The name: {SubscriberName}, you provided  is not valid";

        public string ErrorResponseNotExsistEmail() => $"The subscriber with email address: {SubscriberMail} das not exsist";

        public string ErrorResponseNotValidEmailOnUnsubscribe() => $"Pleace enter a valid email address to unsubscribe";

        public string ErrorResponseEx() => $"An error occurred: {Ex}";

        public string ResponseMailSubscribeSent() => $"\nYoure personal ID is sent to {SubscriberMail}";

        public string ResponseMaiUnsubscribeSent() => $"\nUnsubscription sent to: {SubscriberMail}";

        public string ErrorRresponseMail() => $"\nError sending mail to {SubscriberMail}";
        #endregion

    }
}
