using SAA.Negocio;
using System;
using System.Web.Mvc;
using System.Web.Security;
using SAA.Infra;
using System.Web.Routing;
using SAA.Repository;
using System.Security.Cryptography;

namespace SAA.Authentication.Security
{
    public class TokenValidateActionFilter : ActionFilterAttribute
    {
        private UnitOfWork unit = new UnitOfWork();
        private TokenManager tokenManager;

        public TokenValidateActionFilter()
        {
            tokenManager = new TokenManager(unit);
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var request = filterContext.HttpContext.Request;
            var saaCookie = request.Cookies[FormsAuthentication.FormsCookieName];

            if (null != saaCookie && !string.IsNullOrWhiteSpace(saaCookie.Value))
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket("_", true, 30);
                try
                {
                    ticket = FormsAuthentication.Decrypt(saaCookie.Value);
                    tokenManager.ValidarToken(ticket.UserData, request.UserHostAddress, request.UserAgent);
                }
                catch (CryptographicException)
                {
                    FormsAuthentication.SignOut();
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                         new RouteValueDictionary(
                          new { action = "WebLogin", Controller = "Conta" })));
                }
                catch (Exception)
                {

                    FormsAuthentication.SignOut();
                    tokenManager.RemoveToken(ticket.UserData);
                    IdentityCustom fakeIdentity = new IdentityCustom();
                    filterContext.HttpContext.User = new PrincipalCustom(fakeIdentity);

                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                         new RouteValueDictionary(
                          new { action = "Index", Controller = "Home" })));
                }

            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

    }
}