using System.DirectoryServices.AccountManagement;
using System.Web.Mvc;
using System.Web.Security;
using System.Net;
using System.Web;
using System.Linq;
using SAA.Infra;
using System.Web.Configuration;
using SAA.Negocio;
using SAA.Negocio.Modelos;
using SAA.Repository;
using SAA.Model.Models;
using System;
using System.ComponentModel.DataAnnotations;
using SAA.Model.ViewModel;

namespace SAA.Authentication.Controllers
{
    [RequireHttps]
    public class ContaController : ControllerSAA
    {
        private UnitOfWork unit = new UnitOfWork();
        private TokenManager tokenManager;
        private UsuarioNegocio usuarioNegocio;

        public ContaController()
        {
            tokenManager = new TokenManager(unit);
            usuarioNegocio = new UsuarioNegocio(unit);
        }


        [HttpGet]
        public ActionResult Login(string returnUrl = "/", string AppUrl = "", string AppId = "")
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Redirect(GetAppUrlRedirect(AppId, AppUrl, returnUrl, User.Identity.Name, Request.GetUserToken()));

            if (RequestVemDaExtranet())
                return RedirectToRoute("Default", new { action = "WebLogin", returnUrl = returnUrl, AppUrl = AppUrl, AppId = AppId });
            else
                return RedirectToRoute("Default", new { action = "WindowsLogin", returnUrl = returnUrl, AppUrl = AppUrl, AppId = AppId });
        }

        [HttpGet]
        public ActionResult WindowsLogin(string returnUrl = "/", string AppUrl = "", string AppId = "")
        {
            if (!User.Identity.IsAuthenticated)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            if (IsAuthenticatedByFormsAuthentication())
                return Redirect(GetAppUrlRedirect(AppId, AppUrl, returnUrl, User.Identity.Name, Request.GetUserToken()));

            Usuario user = unit.UsuarioRepository.AllInclude(x => x.StatusUsuario).SingleOrDefault(x => x.Login.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));

            if (null == user || user.LogOut || !user.StatusUsuario.Codigo.Equals("A", StringComparison.InvariantCultureIgnoreCase))
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                return RedirectToRoute("Default", new { action = "WebLogin", returnUrl = returnUrl, AppUrl = AppUrl, AppId = AppId });
            }

            var token = tokenManager.GerarToken(user.Id, Request);

            HttpContext.Response.SetAuthCookie(User.Identity.Name, true, token.Hash);

            LogOutUser(false, user.Login);

            ListAlert.Add(new Alert("Autenticado via Domínio", AlertType.Info));

            return Redirect(GetAppUrlRedirect(AppId, AppUrl, returnUrl, User.Identity.Name, token.Hash));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult WebLogin(string returnUrl, string AppUrl, string AppId)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult WebLogin([Required(ErrorMessage = "Informe o Usuário")][StringLength(5)]string usuario, [Required(ErrorMessage = "Informe a senha")]string senha, string returnUrl = "/", string AppId = "", string appUrl = "", bool domain = true)
        {
            if (ModelState.IsValid)
            {
                string dominio = WebConfigurationManager.AppSettings["SAA-Domain"];

                if (IsAuthenticatedByFormsAuthentication())
                {
                    return Redirect(GetAppUrlRedirect(AppId, appUrl, returnUrl, User.Identity.Name, Request.GetUserToken()));
                }

                if (domain) // 1. Autentica via domínio
                {

                    Status retorno = usuarioNegocio.AutenticaUsuario(usuario, senha, Request, dominio);
                    if (retorno.Succeeded)
                    {

                        string hash = retorno.Token;
                        string userName = dominio + "\\" + usuario;
                        HttpContext.Response.SetAuthCookie(userName, true, hash);
                        LogOutUser(false, userName);
                        return Redirect(GetAppUrlRedirect(AppId, appUrl, returnUrl, userName, hash));
                    }
                }
                else // 2. Autenticação Local
                {

                    Status retorno = usuarioNegocio.AutenticaUsuario(usuario, senha, Request);
                    if (retorno.Succeeded)
                    {

                        string hash = retorno.Token;
                        HttpContext.Response.SetAuthCookie(usuario, true, hash);
                        LogOutUser(false, usuario);
                        return Redirect(GetAppUrlRedirect(AppId, appUrl, returnUrl, usuario, hash));
                    }
                }
            }

            ViewBag.Usuario = usuario;
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult LogOut()
        {
            if (User.Identity.IsAuthenticated)
                LogOutUser(true, User.Identity.Name);

            string hashToken = Request.GetUserToken();
            tokenManager.RemoveToken(hashToken);

            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("/Conta/WebLogin", new { returnURL = "/" });
        }

        [HttpGet]
        [Authorize]
        public ActionResult LogOutAll()
        {
            string hashToken = Request.GetUserToken();
            tokenManager.RemoveAllUserTokens(hashToken);

            FormsAuthentication.SignOut();
            Session.Abandon();

            return Redirect("/Conta/WebLogin");
        }

        private bool RequestVemDaExtranet()
        {
            return Request.Url.DnsSafeHost.IsFQDN();
        }

        private void LogOutUser(bool lembrar, string login)
        {
            var user = unit.UsuarioRepository.SearchFor(x => x.Login.Equals(login, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();

            if (null != user)
            {
                user.LogOut = lembrar;
                unit.UsuarioRepository.Edit(user);
                unit.SaveChanges();
            }
        }

        private bool IsAuthenticatedByFormsAuthentication()
        {
            bool autenticado = false;
            try
            {
                var SAACookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (SAACookie != null && !string.IsNullOrWhiteSpace(SAACookie.Value))
                {
                    var ticket = FormsAuthentication.Decrypt(SAACookie.Value);
                    var hash = ticket.UserData;

                    // Token do usuário ainda está ativo?
                    var token = tokenManager.GetToken(hash);
                    if (token != null)
                        autenticado = true;
                    else
                        autenticado = false;
                }
            }
            catch (Exception)
            {
                // FormsAuthentication.Decrypt pode causar EncripationException
            }

            return autenticado;

        }

        private string GetAppUrlRedirect(string appId, string appUrl, string returnUrl, string login, string userTokenHash)
        {
            if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(appUrl))
                return returnUrl.Contains("conta/login") ? "/" : returnUrl;

            Uri appUri;
            Uri.TryCreate(appUrl, UriKind.Absolute, out appUri);

            if (appUri == null || !appUri.IsAbsoluteUri)
                return returnUrl;


            var aplicacao = unit.AplicacaoRepository.GetByAppId(appId);

            if (aplicacao != null)
            {
                // a url informada está cadatrada para a aplicação?
                if (aplicacao.AplicacaoUrls.Any(x => x.Url.Equals(appUri.Authority, StringComparison.InvariantCultureIgnoreCase)))
                {

                    //Verificar a permissão do usuário
                    var user = unit.UsuarioRepository.GetByLogin(login);
                    bool temPermissao = unit.UsuarioAplicacaoRepository.VerificarPermissaoUser(login, appId);
                    if (temPermissao)
                    {
                        tokenManager.AddAppUserToken(aplicacao.AppId, userTokenHash);
                        // Criptografar User Token
                        Criptografia cripto = new Criptografia();
                        cripto.Key = aplicacao.AppKey;
                        var userTokenEncripted = cripto.Encrypt(userTokenHash);

                        return GetReturnUrlAplicacao(appUri.OriginalString, userTokenEncripted);
                    }
                    else
                        ListAlert.Add(new Alert(string.Format("Você não tem acesso para a aplicação '{0}'", aplicacao.Nome), AlertType.Error));
                }
                else
                    ListAlert.Add(new Alert(string.Format("Url informada para a aplicação '{0}' não foi encontrada no banco de dados", aplicacao.Nome), AlertType.Error));
            }
            else
                ListAlert.Add(new Alert(string.Format("Aplicação '{0}' não encontrada", aplicacao.Nome), AlertType.Error));

            return returnUrl;

        }

        private string GetReturnUrlAplicacao(string urlBase, string encriptedToken)
        {
            return string.Format("{0}&token={1}", urlBase, encriptedToken);
        }

    }
}
