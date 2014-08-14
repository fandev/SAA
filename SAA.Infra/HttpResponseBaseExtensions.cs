using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace SAA.Infra
{
    public static class HttpResponseBaseExtensions
    {
        public static HttpCookie SetAuthCookie(this HttpResponseBase responseBase, string name, bool rememberMe, string userData)
        {
            /// In order to pickup the settings from config, we create a default cookie and use its values to create a 
            /// new one.
            try
            {
                var cookie = FormsAuthentication.GetAuthCookie(name, rememberMe);
                var ticket = FormsAuthentication.Decrypt(cookie.Value);

                var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration,
                    false, userData, ticket.CookiePath);
                var encTicket = FormsAuthentication.Encrypt(newTicket);

                /// Use existing cookie. Could create new one but would have to copy settings over...
                cookie.Secure = true;
                cookie.Value = encTicket;
                cookie.HttpOnly = true;
                cookie.Expires = DateTime.Now.AddMonths(1);
                responseBase.Cookies.Add(cookie);

                return cookie;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
