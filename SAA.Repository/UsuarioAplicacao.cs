using SAA.Infra;
using SAA.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Repository
{
    public class UsuarioAplicacaoRepository : RepositoryBase<UsuarioAplicacao>
    {
        public UsuarioAplicacaoRepository(DbContext context) : base(context) { }

        public bool VerificarPermissaoUser(string login, string appId)
        {
            return All()
                .Any(x => x.Usuario.Login.Equals(login, StringComparison.InvariantCultureIgnoreCase)
                    && x.Aplicacao.AppId.Equals(appId, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
