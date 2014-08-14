using System;
using System.Linq;
using System.Web.Http;
using SAA.Infra;
using SAA.Model.ViewModel;
using SAA.Repository;
using System.Threading.Tasks;

namespace SAA.Authorization.Controllers
{
    public class PermissaoAcaoController : APIControllerBase, IAPIController<Result>
    {

        private UnitOfWork db = new UnitOfWork();
        private Negocio.PermissaoNegocio negocio = new Negocio.PermissaoNegocio();

        /// <summary>
        /// Retorna um Result com informações de permissão para a ação solicitada
        /// </summary>
        /// <param name="id">hash da rota da ação</param>
        /// <returns></returns>
        [AllowAnonymous]
        public Task<Result> Get(string id)
        {
            var acao = db.AcaoRepository.GetByHash(id);
            
            var permissao = db.FuncionarioPerfilPermissaoRepository.GetPermissaoByUserIdAppGUIDAcaoHash(this.FuncionarioId, this.AppGUID, id);

            var result = negocio.VerificaPermissaoAcao(acao, permissao);

            return Task<Result>.Factory.StartNew(() => result);
        }

        public Task<IHttpActionResult> Post(Result item)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> Put(Result item)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> Delete(object id)
        {
            throw new NotImplementedException();
        }


        public IQueryable<Result> Get()
        {
            throw new NotImplementedException();
        }
    }
}
