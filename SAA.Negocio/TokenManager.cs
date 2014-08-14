using SAA.Infra;
using SAA.Model.Models;
using SAA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using SAA.Model.ViewModel;

namespace SAA.Negocio
{
    public class TokenManager
    {
        public static List<Token> ListTokens;

        /// <summary>
        /// key: userToken Hash
        /// AppUseToken: 
        /// </summary>
        public static Dictionary<string, AppUserToken> AppUserTokens;

        public static ResourceManager resourceManagerMsgs = new ResourceManager("SAA.Negocio.Properties.i18n.Message", Assembly.GetExecutingAssembly());

        private UnitOfWork unit;

        public TokenManager(UnitOfWork _unit)
        {
            this.unit = _unit;
        }


        #region User Token
        public Token GerarToken(int idUsuario, System.Web.HttpRequestBase request)
        {
            Token token = new Token();
            token.IdUsuario = idUsuario;
            token.IP = request.UserHostAddress;
            token.UserAgent = request.UserAgent.ComputeHash(Infra.HashHelper.eHashType.MD5);
            token.DataCriacao = DateTime.Now;
            token.Hash = Guid.NewGuid().ToString("N");
            unit.TokenRepository.Insert(token);
            unit.SaveChanges();
            ListTokens.Add(token);
            return token;
        }

        public void AddAppUserToken(string appId, string userToken)
        {

            try
            {
                var _aplicacao = unit.AplicacaoRepository.GetByAppId(appId);

                var _userToken = GetToken(userToken);

                if (_userToken != null && _aplicacao != null)
                {

                    // persiste em caso de parado do serviço
                    UserTokenAppToken _userTokenAppToken = new UserTokenAppToken
                    {
                        IdAplicacao = _aplicacao.Id,
                        IdUserToken = _userToken.Id,
                        DataExpiracao = DateTime.Now.AddMinutes(10),
                        DataRegistro = DateTime.Now
                    };

                    // armazena na lista de cache
                    AppUserToken _appUserToken = new AppUserToken
                    {
                        AppId = appId.ToLower(),
                        UserToken = userToken.ToLower(),
                        Expiration = DateTime.Now.AddMinutes(10)
                    };

                    unit.UserTokenAppTokenRepository.Insert(_userTokenAppToken);
                    unit.SaveChanges();
                    AppUserTokens.Add(_userToken.Hash + appId, _appUserToken);
                }
            }
            catch (Exception)
            {

            }
        }

        // call of SSO Authentication
        public void ValidarToken(string hash, string IP, string userAgent)
        {

            if (string.IsNullOrEmpty(hash.Trim()) || string.IsNullOrEmpty(IP.Trim()) || string.IsNullOrEmpty(userAgent.Trim()))
                throw new ArgumentException(resourceManagerMsgs.GetString("ArgumentException"));

            var token = GetToken(hash);
            if (token != null)
            {
                var user = unit.UsuarioRepository.AllInclude(x => x.StatusUsuario).SingleOrDefault(x => x.Id == token.IdUsuario);
                if (user != null)
                {
                    if (!user.StatusUsuario.Codigo.Equals("A", StringComparison.InvariantCultureIgnoreCase))
                        throw new Exception(resourceManagerMsgs.GetString("DisabledUser"));
                }
                else
                    throw new Exception(resourceManagerMsgs.GetString("UserNotFound"));

                ValidarIp(token, IP);
                ValidarUserAgent(token, userAgent);
            }
            else
                throw new Exception(resourceManagerMsgs.GetString("UserTokenInvalid"));
        }
        
        // call of applications
        public void ValidarToken(string userToken, string appId)
        {
            if (string.IsNullOrEmpty(userToken.Trim()) || string.IsNullOrWhiteSpace(appId.Trim()))
                throw new ArgumentException(resourceManagerMsgs.GetString("ArgumentException"));

            if (!IsAppUserSessionActive(appId, userToken))
            {
                RemoveToken(userToken, appId);
                throw new Exception(resourceManagerMsgs.GetString("UserSessionExpired"));
            }

            var userTokenAppToken = unit.UserTokenAppTokenRepository.AllInclude(x=> x.Token).SingleOrDefault(x=> x.Token.Hash.Equals(userToken, StringComparison.InvariantCultureIgnoreCase));
            var user = unit.UsuarioRepository.AllInclude(x => x.StatusUsuario).SingleOrDefault(x => x.Id == userTokenAppToken.Token.IdUsuario);
            if (user != null)
            {
                if (!user.StatusUsuario.Codigo.Equals("A", StringComparison.InvariantCultureIgnoreCase))
                    throw new Exception(resourceManagerMsgs.GetString("DisabledUser"));
            }
            else
                throw new Exception(resourceManagerMsgs.GetString("UserNotFound"));
        }

        private void ValidarIp(Token token, string IP)
        {
            if (!(token.IP.Equals(IP, StringComparison.InvariantCultureIgnoreCase)))
                throw new Exception(resourceManagerMsgs.GetString("IPAddressInvalid"));
        }

        private void ValidarUserAgent(Token token, string userAgent)
        {
            if (!token.UserAgent.Equals(userAgent.ComputeHash(Infra.HashHelper.eHashType.MD5), StringComparison.InvariantCultureIgnoreCase))
                throw new Exception(resourceManagerMsgs.GetString("UserAgentInvalid"));
        }

        public Token GetToken(string Hash)
        {
            var token = ListTokens.SingleOrDefault(x => x.Hash.Equals(Hash, StringComparison.InvariantCultureIgnoreCase));
            if (token == null)
            {
                token = unit.TokenRepository.GetToken(Hash);
                if (token != null)
                    ListTokens.Add(token);
            }
            return token;
        }

        public bool IsAppUserSessionActive(string appId, string userToken)
        {
            bool sessaoAtiva = false;
            AppUserToken appUserToken;
            AppUserTokens.TryGetValue(userToken + appId, out appUserToken);

            UserTokenAppToken userTokenAppToken;
            if (appUserToken != null && appUserToken.Expiration > DateTime.Now)
                sessaoAtiva = true;
            else
            {
                userTokenAppToken = unit.UserTokenAppTokenRepository.GetByUserTokenHash(userToken);
                if (userTokenAppToken != null && userTokenAppToken.DataExpiracao > DateTime.Now)
                    sessaoAtiva = true;
            }

            return sessaoAtiva;
        }

        // SSO LogOut
        public void RemoveToken(string hash)
        {
            try
            {
                ListTokens.RemoveAll(x => x.Hash.Equals(hash));
                unit.TokenRepository.DeleteRange(x => x.Hash.Equals(hash));
                unit.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        // APP LogOut
        public void RemoveToken(string hash, string appId)
        {
            try
            {
                AppUserTokens.Remove(hash + appId); // remove from cache
                unit.UserTokenAppTokenRepository.DeleteRange(x =>
                    x.Token.Hash.Equals(hash, StringComparison.InvariantCultureIgnoreCase)
                    && x.Aplicacao.AppId.Equals(appId, StringComparison.InvariantCultureIgnoreCase)); // remove from database
            }
            catch (Exception e)
            {

            }
        }

        // LogOut All places
        public void RemoveAllUserTokens(string hash)
        {
            try
            {
                var token = GetToken(hash);
                if (token != null)
                {
                    ListTokens.RemoveAll(x => x.IdUsuario == token.IdUsuario);
                    unit.TokenRepository.DeleteRange(x => x.IdUsuario == token.IdUsuario);
                }
                else
                {
                    var appUserTokensToRemove = AppUserTokens.Where(x => x.Key.Contains(hash));
                    foreach (var item in appUserTokensToRemove)
                    {
                        AppUserTokens.Remove(item.Key);
                    }
                }

                unit.UserTokenAppTokenRepository.DeleteRange(x => x.Token.Hash.Equals(hash, StringComparison.InvariantCultureIgnoreCase));
                unit.SaveChanges();
            }
            catch (Exception)
            {

            }
        }


        public void AumentaTempoVidaToken(string userToken, string appId)
        {
            AppUserToken appUserToken;
            AppUserTokens.TryGetValue(userToken + appId, out appUserToken);

            if (appUserToken == null)
            {
                var userTokenAppToken = unit.UserTokenAppTokenRepository.GetByUserTokenHash(userToken);
                if (userTokenAppToken != null)
                {
                    userTokenAppToken.DataExpiracao = DateTime.Now.AddMinutes(10);
                    unit.UserTokenAppTokenRepository.Edit(userTokenAppToken);
                    unit.SaveChanges();

                    // adiciona no cache
                    appUserToken = new AppUserToken
                    {
                        AppId = appId,
                        UserToken = userToken,
                    };
                    AppUserTokens.Add(userToken + appId, appUserToken);
                }
            }

            if (appUserToken != null)
            {
                appUserToken.Expiration = DateTime.Now.AddMinutes(10);
            }
            else
                throw new Exception(resourceManagerMsgs.GetString("UserTokenInvalid"));
        }

        #endregion

        #region Application Token
        public void ValidarAppToken(string SAA_APPToken)
        {
            var aplicacao = unit.AplicacaoRepository.GetByAppKey(SAA_APPToken);
            if (aplicacao == null)
                throw new Exception(resourceManagerMsgs.GetString("AppTokenInvalid"));
            if (aplicacao.DataExpiracao < DateTime.Now)
                throw new Exception(resourceManagerMsgs.GetString("AppTokenExpired"));
        }

        public void ValidarPermissaoAppClient(string appKey, string appId)
        {
            bool permitido = unit.AplicacaoRepository.Existe(appId, appKey);

            if (!permitido)
                throw new UnauthorizedAccessException(resourceManagerMsgs.GetString("AppClientAccessDenied"));
        }

        public void ValidarAppGUID(string AppGuid)
        {
            bool isValid = unit.AplicacaoRepository.All().Any(x => x.AppId.Equals(AppGuid, StringComparison.InvariantCultureIgnoreCase));
            if (!isValid)
                throw new Exception(resourceManagerMsgs.GetString("AppGUIDInvalid"));
        }

        #endregion

    }
}
