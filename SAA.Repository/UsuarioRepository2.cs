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
    public class UsuarioRepository : RepositoryBase<Usuario>
    {
        public UsuarioRepository(DbContext context) : base(context) { }

        public Usuario GetByLogin(string login)
        {
            return AllInclude(x => x.StatusUsuario, y => y.Funcionario).SingleOrDefault(x => x.Login.Equals(login, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
