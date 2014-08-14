using SAA.Infra;
using SAA.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Repository
{
    public class FuncionarioPerfilPermissaoRepository : RepositoryBase<FuncionarioPerfilPermissao>
    {
        public FuncionarioPerfilPermissaoRepository(DbContext context) : base(context) { }

        public FuncionarioPerfilPermissao GetPermissaoByUserIdAppGUIDAcaoHash(int funcionarioId, string appId, string acaoHash)
        {
            var permissao = SearchFor(x => x.FuncionarioPerfil.Funcionario.Id == funcionarioId && x.Acao.Transacao.Aplicacao.AppId.Equals(appId) && x.Acao.Hash.Equals(acaoHash));
            return permissao.SingleOrDefault();
        }

        public List<FuncionarioPerfilPermissao> GetPermissaoByUserIdAppGUIDTransacaoHash(int userId, string appId, string transacaoHash)
        {
            var permissao = SearchFor(x => x.FuncionarioPerfil.Funcionario.Id == userId && x.FuncionarioPerfil.PerfilAplicacao.Aplicacao.AppId.Equals(appId) && x.Acao.Transacao.Hash.Equals(transacaoHash));
            return permissao.ToList();
        }
    }
}
