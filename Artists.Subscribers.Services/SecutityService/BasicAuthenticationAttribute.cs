using Artists.Subscribers.Data.Validation;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Artists.Subscribers.Services.UsersSecurity
{

    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                #region Decoding

                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                string[] userPassArray = decodedAuthenticationToken.Split(':');
                string username = userPassArray[0];
                string password = userPassArray[1];

                #endregion

                
               
                if (ValidationMetods.ValidSubscriberName(username))
                {                 

                    if (UserSecurity.LogIn(username, password))
                    {
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

            }
        }
    }
}
