using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artists.Subscribers.Services.MailService
{
    interface IMailService
    {
        string Sender { get; set; }

        string Password { get; set; }

        string SmtpClient { get; set; }

        string SmtpPort { get; set; }

        string MailSubject { get; set; }

        string MailBodySubscribed { get; set; }

        string MailBodyUnsubscribed { get; set; }

        string RecipientMailAddress { get; set; }

        string RecipientName { get; set; }

        string RecipientID { get; set; }


        void MailSettings
             (
                string sender,
                string password, 
                string smtpClient,
                string smtpPort,
                string mailSubject,
                string mailBodySubscribed,
                string mailBodyUnsubscribed
             );

        bool SendMail();
    }
}
