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
    public class AcaoRepository : RepositoryBase<Acao>
    {
        public AcaoRepository(DbContext context) : base(context) { }

        public Acao GetByHash(string hash)
        {
            return AllInclude(x => x.Transacao).SingleOrDefault(x => x.Hash.Equals(hash));
        }
    }
}
