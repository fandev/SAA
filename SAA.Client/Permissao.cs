using RestSharp;
using SAA.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Client
{
    public class Permissao
    {
        private RestClientConfig restClient = new RestClientConfig();
        public Model.ViewModel.Result VerificarPermissaoAcao(string hash, string tokenUsuario)
        {
            // retorna um request com o token da App client
            var request = restClient.GetDefaultRequest(tokenUsuario);
            request.Method = RestSharp.Method.GET;
            request.Resource = "/api/PermissaoAcao/{hash}";
            request.AddUrlSegment("hash", hash);
            var response = restClient.Client.Execute<Result>(request);

            return VerificarErros(response);
        }

        public Result VerificarPermissoesTransacao(string hash, string tokenUsuario)
        {
            var request = restClient.GetDefaultRequest(tokenUsuario);
            request.Method = RestSharp.Method.GET;
            request.Resource = "/api/PermissaoTransacao/{hash}";
            request.AddUrlSegment("hash", hash);
            var response = restClient.Client.Execute<Result>(request);

            return VerificarErros(response);
        }

        private Result VerificarErros(IRestResponse<Result> response)
        {
            if (response.ErrorException != null)
                throw new Exception(response.ErrorMessage);

            // Verifica se a chamada ao serviço foi executada com sucesso
            if (response.ResponseStatus != RestSharp.ResponseStatus.Completed)
                return new Result { Codigo = StatusResult.Exception.ToString() , Mensagem = response.ErrorMessage };
            return response.Data;
        }


    }
}
