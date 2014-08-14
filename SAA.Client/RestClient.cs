using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Runtime.InteropServices;
using System.Reflection;
using SAA.Model.ViewModel;
using SAA.Infra;

namespace SAA.Client
{
    internal class RestClientConfig
    {
        private RestSharp.RestClient _client;

        public RestSharp.RestClient Client
        {
            get{ return _client;}
            private set { _client = value; }
        }
        internal RestClientConfig()
        {
            // recupera a url da API da seção AppSettings do arquivo de configuração do assembly que está executando
            var urlBase = GetValeuOfSectionAppSettingsOfFileOfConfigCurrentAssemblyRunning("SAA-Location");
            _client = new RestSharp.RestClient(urlBase);
        }
        internal RestClientConfig(string urlBase)
        {
            _client = new RestSharp.RestClient(urlBase);
        }

        /// <summary>
        /// Retorna uma instância de RestSharp.RestRequest com o headers apropriados para o serviço
        /// </summary>
        /// <param name="recurso">ex:. /api/TransacaoAcao/{hash}. user RestRequest.AddUrlSegment para substituir a string {hash}</param>
        /// <returns></returns>
        internal RestSharp.RestRequest GetDefaultRequest(string tokenUsuario)
        {
            RestSharp.RestRequest request = new RestRequest();

            // adiciona ao HEADER o token do Usuário
            request.AddHeader("SAA-Token", tokenUsuario);

            // adiciona ao HEADER o guid do assembly que está executando
            var appGuid = Assembly.GetExecutingAssembly().GetType().GUID.ToString("N");
            request.AddHeader("SAA-AppGUID", appGuid);

            // adiciona ao HEADER o token do aplicativo client da API
            var appToken = GetValeuOfSectionAppSettingsOfFileOfConfigCurrentAssemblyRunning("SAA-AppToken");
            request.AddHeader("SAA-AppToken", appToken);

            return request;
        }

        /// <summary>
        /// Retorna um valor de uma chave da seção AppSettins do arquivo xml de configuração do Assembly está executando
        /// </summary>
        /// <param name="KeyCustomSetting"></param>
        /// <returns></returns>
        private string GetValeuOfSectionAppSettingsOfFileOfConfigCurrentAssemblyRunning(string KeyCustomSetting)
        {
            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/");

            // cria um elemento chave valor para App
            System.Configuration.KeyValueConfigurationElement customSetting = new System.Configuration.KeyValueConfigurationElement(KeyCustomSetting, "");

            if (rootWebConfig.AppSettings.Settings.Count > 0)
                customSetting = rootWebConfig.AppSettings.Settings[KeyCustomSetting];
            else
                throw new Exception(String.Format("A chave {0} não foi encontrada no arquivo de configuração da aplicação", KeyCustomSetting));

            if (customSetting == null)
                throw new Exception(String.Format("A chave {0} não possui valor.", KeyCustomSetting));

            return customSetting.Value;
        }

    }
}
