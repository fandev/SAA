using System.Collections.Generic;

namespace SAA.Interface.Controllers
{
    interface IPermissaoAcaoController
    {
        /// <summary>
        /// Recupera uma ação com base no seu hash
        /// </summary>
        /// <param name="id">identificação da ação</param>
        /// <returns>lista com uma acao + codigo e mensagem de retorno ou null</returns>
        SAA.Model.ViewModel.Result Get(string id);
    }
}
