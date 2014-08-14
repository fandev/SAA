using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace SAA.Infra
{
    public static class HttpRequestBaseExtensions
    {
        public static HttpCookie GetAuthCoolie(this HttpRequestBase requestBase)
        {
            return requestBase.Cookies[FormsAuthentication.FormsCookieName];
        }

        public static string GetUserToken(this HttpRequestBase requestBase)
        {
            try
            {
                var cookie = requestBase.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null && !string.IsNullOrWhiteSpace(cookie.Value))
                    return FormsAuthentication.Decrypt(cookie.Value).UserData;
                else
                    return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}
