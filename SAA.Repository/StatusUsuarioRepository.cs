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
    public class StatusUsuarioRepository : RepositoryBase<StatusUsuario>
    {
        public StatusUsuarioRepository(DbContext context) : base(context)
        {

        }

        public StatusUsuario GetStatusUsuario(int id)
        {
            var status = SearchFor(x => x.Id == id).SingleOrDefault();
            if (status != null)
                return status;
            return null;
        }
    }
}

