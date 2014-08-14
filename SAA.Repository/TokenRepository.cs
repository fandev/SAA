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
    public class TokenRepository : RepositoryBase<Token>
    {
        public TokenRepository(DbContext context)
            : base(context)
        {

        }
       public Token GetToken(string Hash)
        {
            return  SearchFor(x => x.Hash.Equals(Hash, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();
        }

       public Token GetToken(int idUsuario, string ip, string userAgent)
       {
           return SearchFor(x => x.IdUsuario == idUsuario
               && x.Id.Equals(ip)
               && x.UserAgent.Equals(userAgent.ComputeHash(HashHelper.eHashType.MD5))).FirstOrDefault();
       }

    }
}
