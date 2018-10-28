using System;
using System.Net.Mail;
using Artists.Subscribers.Services.MailService;


namespace Artists.Subscribers.WebApi.Mail
{
    public class SubscribersMailService : IMailService
    {

        #region Properties

        public string Sender { get; set; }

        public string Password { get; set; }

        public string SmtpClient { get; set; }

        public string SmtpPort { get; set; }

        public string MailSubject { get; set; }

        public string MailBodySubscribed { get; set; }

        public string MailBodyUnsubscribed { get; set; }

        public string RecipientMailAddress { get; set; }

        public string RecipientName { get; set; }

        public string RecipientID { get; set; }

        #endregion


        #region Constructors

        public SubscribersMailService(string recipientName, string recipientMail)
        {
            this.RecipientName = recipientName;
            this.RecipientMailAddress = recipientMail;
        }

        public SubscribersMailService(string recipientId, string recipientName, string recipientMail)
        {
            this.RecipientID = recipientId;
            this.RecipientName = recipientName;
            this.RecipientMailAddress = recipientMail;
        }

        public SubscribersMailService() { }

        #endregion

        #region Metods

        public void MailSettings(string sender, string password, string smtpClient, string smtpPort, string mailSubject, string mailBodySubscribed, string mailBodyUnsubscribed)
        {
            Sender = sender;
            Password = password;
            SmtpClient = smtpClient;
            SmtpPort = smtpPort;
            MailSubject = mailSubject;
            MailBodySubscribed = mailBodySubscribed;
            MailBodyUnsubscribed = mailBodyUnsubscribed;          
        }

        public bool SendMail()
        {  
            // two mail body type options : subscribe or unsubscribe
            var mailBodyType = MailBodySubscribed != string.Empty ? MailBodySubscribed + $"\nYore personal ID: {RecipientID}"
                                                              : MailBodyUnsubscribed;

            string bodyText = $"Dear {RecipientName}" + Environment.NewLine + "\n" +mailBodyType;

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(SmtpClient);

            mail.From = new MailAddress(Sender);
            mail.To.Add(RecipientMailAddress);
            mail.Subject = MailSubject;
            mail.Body = bodyText;
            SmtpServer.Port = Convert.ToInt32(SmtpPort);
            #region Credentials
            SmtpServer.Credentials = new System.Net.NetworkCredential(Sender, Password);
            #endregion
            SmtpServer.EnableSsl = false;

            try
            {
                SmtpServer.Send(mail);
                return true;
            }
            catch (SmtpFailedRecipientException)
            {
                return false;
            }

        }

        #endregion


       

      
    }
}