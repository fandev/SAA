using SAA.Infra;
using SAA.Model.Models;
using SAA.Negocio;
using SAA.Repository;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SAA.Authorization.Security
{
    public class TokenInspector : DelegatingHandler
    {
        private UnitOfWork unit = new UnitOfWork();
        private TokenManager tokenManager;

        public TokenInspector()
        {
            tokenManager = new TokenManager(unit);
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {

            const string SAA_UserToken = "SAA-Token"; // Token do Usuário
            const string SAA_APPToken = "SAA-AppToken"; // Token do Aplicativo ou serviço requerente
            const string SAA_APPGUID = "SAA-AppGUID"; // Identificação do Aplicativo ou serviço do qual se quer consultar permissões de usuários

            string userToken = "";
            string SAA_AppToken = "";
            string SAA_AppId = "";
            Token token;

            if (request.Headers.Contains(SAA_UserToken) && request.Headers.Contains(SAA_APPGUID) && request.Headers.Contains(SAA_APPToken))
            {
                userToken = request.Headers.GetValues(SAA_UserToken).First();
                SAA_AppToken = request.Headers.GetValues(SAA_APPToken).First();
                SAA_AppId = request.Headers.GetValues(SAA_APPGUID).First();

                try
                {

                    tokenManager.ValidarAppGUID(SAA_AppId);

                    tokenManager.ValidarAppToken(SAA_AppToken);

                    tokenManager.ValidarToken(userToken, SAA_AppId);

                    tokenManager.AumentaTempoVidaToken(userToken, SAA_AppId);

                    token = tokenManager.GetToken(userToken);

                }
                catch (Exception e)
                {
                    HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.Forbidden, e.Message);
                    return Task.FromResult(reply);
                }
            }
            else
            {
                HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Faltam parâmetros na chamada");
                return Task.FromResult(reply);
            }

            // Atribui o Id do funcionário no cabeçalho da request request
            request.Headers.Add("SAA-userId", token.Usuario.Id.ToString());
            request.Headers.Add("SAA-funcionarioId", token.Usuario.IdFuncionario.ToString());
            return base.SendAsync(request, cancellationToken);
        }
    }
}