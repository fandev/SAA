using RestSharp;
using SAA.Infra;
using SAA.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Client
{
    public class Autenticacao
    {
        private RestClientConfig restClient = new RestClientConfig();
        public Model.ViewModel.UserInfo GetUseInfo(string tokenUsuario)
        {
            // retorna um request com o token da App client
            var request = restClient.GetDefaultRequest(tokenUsuario);
            request.Method = RestSharp.Method.GET;
            request.Resource = "/api/Account/{id}";
            request.AddUrlSegment("id", tokenUsuario);
            var response = restClient.Client.Execute<UserInfo>(request);

            return VerificarErros(response);
        }

        private UserInfo VerificarErros(IRestResponse<UserInfo> response)
        {
            if (response.ErrorException != null)
                throw new Exception(response.ErrorMessage);

            // Verifica se a chamada ao serviço foi executada com sucesso
            if (response.ResponseStatus != RestSharp.ResponseStatus.Completed)
                throw new Exception("Não foi possível recuperar informações do usuário");

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                throw new Exception(response.StatusDescription);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new Exception(response.StatusDescription);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.StatusDescription);

            return response.Data;
        }


    }
}
