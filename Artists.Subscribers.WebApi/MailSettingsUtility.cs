using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Artists.Subscribers.WebApi
{
    public class MailSettingsUtility
    {
        public string Sender { get; set; }
        public string Password { get; set; }
        public string SmtpClient { get; set; }
        public string SmtpPort { get; set; }
        public string Subject { get; set; }
        public string BodySubscribe { get; set; }
        public string BodyUnsubscribe { get; set; }


        public MailSettingsUtility()
        {
            Sender = SenderGet();
            Password = PasswordGet();
            SmtpClient = SmtpClientGet();
            SmtpPort = SmtpPortGet();
            Subject = SubjectGet();
            BodySubscribe = BodySubscribeGet();
            BodyUnsubscribe = BodyUnsubscribeGet();
        }

        private string SenderGet() => ConfigurationManager.AppSettings["Sender"].ToString();
        private string PasswordGet() => ConfigurationManager.AppSettings["Password"].ToString();
        private string SmtpClientGet() => ConfigurationManager.AppSettings["smtpClient"].ToString();
        private string SmtpPortGet() => ConfigurationManager.AppSettings["SmtpPort"].ToString();
        private string SubjectGet() => ConfigurationManager.AppSettings["Subject"].ToString();
        private string BodySubscribeGet() => ConfigurationManager.AppSettings["BodySubscribe"].ToString();
        private string BodyUnsubscribeGet() => ConfigurationManager.AppSettings["BodyUnsubscribe"].ToString();
    }
}