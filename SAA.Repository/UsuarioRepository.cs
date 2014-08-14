using SAA.Infra;
using SAA.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Repository
{
    public class UsuarioRepository:RepositoryBase<Usuario>
    {
        public UsuarioRepository(KERBEROSContext context): base(context)
        {
            
        }

        public Usuario GetUsuario(string Login, string Senha)
        {
            Usuario user = SearchFor(x => x.Login.Equals(Login) && x.Senha.Equals(Senha)).SingleOrDefault();
            if (user != null)
                return user;
            return null;
        }

    }
}
