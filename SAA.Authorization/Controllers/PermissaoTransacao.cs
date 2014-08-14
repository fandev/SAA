using SAA.Infra;
using SAA.Model.Models;
using SAA.Model.ViewModel;
using SAA.Negocio;
using SAA.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SAA.Authorization.Controllers
{
    [AllowAnonymous()]
    public class PermissaoTransacaoController: APIControllerBase, IAPIController<Result>
    {
        private UnitOfWork db = new UnitOfWork();
        private PermissaoNegocio negocio = new PermissaoNegocio();

        public IQueryable<Result> Get()
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<Result> Get(string id)
        {

            Transacao transacao = db.TransacaoRepository.AllInclude(x => x.Acaos, a=> a.Aplicacao).SingleOrDefault(x => x.Hash.Equals(id) && x.Aplicacao.AppId.Equals(this.AppGUID));

            var permissoes = db.FuncionarioPerfilPermissaoRepository.GetPermissaoByUserIdAppGUIDTransacaoHash(this.FuncionarioId, this.AppGUID, id);

            var result = negocio.VerificaPermissaoTransacao(transacao, permissoes);

            return Task<Result>.Factory.StartNew(() => result);
        }

        public System.Threading.Tasks.Task<System.Web.Http.IHttpActionResult> Post(Result item)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<System.Web.Http.IHttpActionResult> Put(Result item)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<System.Web.Http.IHttpActionResult> Delete(object id)
        {
            throw new NotImplementedException();
        }
    }
}