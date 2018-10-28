using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using Artists.Subscribers.WebApi.Mail;
using Artists.Subscribers.Data.Validation;
using Artists.Subscribers.DataAccess.DTO;
using Artists.Subscribers.DataAccess.Models;
using Artists.Subscribers.Services.UsersSecurity;
using System.Configuration;

namespace Artists.Subscribers.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class SubscriberController : ApiController
    {
        #region Private fields
        private SubscribersDBEntities subscribersEntities;
        private SubscribersMailService mailService;
        private SubscribersResponseMessages responseMessage;
        private readonly MailSettingsUtility mailSettings = new MailSettingsUtility();
        #endregion

        [BasicAuthentication]
        public HttpResponseMessage Get()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (subscribersEntities = new SubscribersDBEntities())
            {
                switch (username.ToLower())
                {
                    case UserRoles.Admin:
                        var adminRequest = subscribersEntities.TopArtistsSubscribers;
                        return Request.CreateResponse(HttpStatusCode.OK, adminRequest.ToList());

                    case UserRoles.Employee:
                        var employeeRequest = from subscribers in subscribersEntities.TopArtistsSubscribers
                                              select new SubscribersDto
                                              {
                                                  SubscriberEmail = subscribers.SubscriberEmail,
                                                  SubscriberName = subscribers.SubscriberName
                                              };
                        return Request.CreateResponse(HttpStatusCode.OK, employeeRequest.ToList());

                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }

        [AllowAnonymous]
        public HttpResponseMessage Post([FromBody] TopArtistsSubscribers subscriber)
        {
            #region Create response messages
            responseMessage = new SubscribersResponseMessages
            (
                subscriberMail: subscriber.SubscriberEmail,
                subscriberName: subscriber.SubscriberName
            );
            #endregion

            bool isValidEmali = ValidationMetods.ValidSubscriberEmail(subscriber.SubscriberEmail);
            bool isValidName = ValidationMetods.ValidSubscriberName(subscriber.SubscriberName);

            if (isValidEmali && isValidName)
            {
                try
                {
                    using (subscribersEntities = new SubscribersDBEntities())
                    {
                        var mailExsist = subscribersEntities.TopArtistsSubscribers.Any(
                                         m => m.SubscriberEmail == subscriber.SubscriberEmail);
                        if (!mailExsist)
                        {
                            var newSubscriber = new TopArtistsSubscribers
                            {
                                Id = Guid.NewGuid(),
                                SubscriberName = subscriber.SubscriberName.Trim(),
                                SubscriberEmail = subscriber.SubscriberEmail.Trim(),
                                SubscribtionDateCreated = DateTime.UtcNow
                            };

                            subscribersEntities.TopArtistsSubscribers.Add(newSubscriber);
                            subscribersEntities.SaveChanges();

                            #region Send Email

                          //  var sender = ConfigurationManager.AppSettings.AllKeys.GetValue;

                            mailService = new SubscribersMailService
                            (
                                recipientId: newSubscriber.Id.ToString(),
                                recipientName: newSubscriber.SubscriberName,
                                recipientMail: newSubscriber.SubscriberEmail
                            );
                            mailService.MailSettings
                            (
                                sender: mailSettings.Sender,
                                password: mailSettings.Password,
                                smtpClient: mailSettings.SmtpClient,
                                smtpPort: mailSettings.SmtpPort,
                                mailSubject: mailSettings.Subject,
                                mailBodySubscribed: mailSettings.BodySubscribe,
                                mailBodyUnsubscribed: string.Empty
                            );
                            string mailSent = mailService.SendMail() == true ? responseMessage.ResponseMailSubscribeSent()
                                                                             : responseMessage.ErrorRresponseMail();

                            #endregion

                            return Request.CreateResponse(HttpStatusCode.OK, responseMessage.ResponseCreated() + mailSent);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Conflict, responseMessage.ErrorResponseIsRegistrated());
                        }
                    }

                }
                catch (Exception ex)
                {
                    responseMessage.Ex = ex.Message;
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, responseMessage.ErrorResponseEx());
                }
            }
            else
            {
                if (!isValidEmali && !isValidName)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, responseMessage.ErrorResponseNotValidNameAndEmail());
                }
                else if (!isValidEmali)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, responseMessage.ErrorResponseNotValidEmail());
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, responseMessage.ErrorResponseNotValidName());
                }
            }
        }

        [AllowAnonymous]
        public HttpResponseMessage Delete([FromBody] TopArtistsSubscribers subscriber)
        {
            #region Create response messages
            responseMessage = new SubscribersResponseMessages
            (
                subscriberMail: subscriber.SubscriberEmail,
                subscriberName: subscriber.SubscriberName
            );
            #endregion

            bool isValidEmali = ValidationMetods.ValidSubscriberEmail(subscriber.SubscriberEmail);

            if (isValidEmali)
            {
                try
                {
                    using (subscribersEntities = new SubscribersDBEntities())
                    {
                        var mailExsist = subscribersEntities.TopArtistsSubscribers.SingleOrDefault(
                                         m => m.SubscriberEmail == subscriber.SubscriberEmail.Trim());

                        if (mailExsist == null)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, responseMessage.ErrorResponseNotExsistEmail());
                        }
                        else
                        {
                            var exSubscriberName = subscribersEntities.TopArtistsSubscribers.SingleOrDefault(
                                                   s => s.SubscriberEmail == subscriber.SubscriberEmail)?.SubscriberName;

                            subscribersEntities.TopArtistsSubscribers.Remove(mailExsist);
                            subscribersEntities.SaveChanges();

                            #region Send email
                            mailService = new SubscribersMailService
                            (
                                 recipientName: exSubscriberName,
                                 recipientMail: subscriber.SubscriberEmail
                            );
                            mailService.MailSettings
                            (
                               sender: mailSettings.Sender,
                               password: mailSettings.Password,
                               smtpClient: mailSettings.SmtpClient,
                               smtpPort: mailSettings.SmtpPort,
                               mailSubject: mailSettings.Subject,
                               mailBodySubscribed: string.Empty,
                               mailBodyUnsubscribed: mailSettings.BodyUnsubscribe
                            );
                            string mailSent = mailService.SendMail() == true ? responseMessage.ResponseMaiUnsubscribeSent()
                                                                             : responseMessage.ErrorRresponseMail();
                            #endregion

                            return Request.CreateResponse(HttpStatusCode.OK, responseMessage.ResponseDeleted() + mailSent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    responseMessage.Ex = ex.Message;
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, responseMessage.ErrorResponseEx());
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, responseMessage.ErrorResponseNotValidEmailOnUnsubscribe());
            }
        }
    }
}
