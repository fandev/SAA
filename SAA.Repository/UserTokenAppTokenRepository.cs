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
    public class UserTokenAppTokenRepository : RepositoryBase<UserTokenAppToken>
    {
        public UserTokenAppTokenRepository(DbContext context) : base(context) { }

        public UserTokenAppToken GetByUserTokenHash(string hash)
        {
            return SearchFor(x => x.Token.Hash.Equals(hash, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();
        }

        public UserTokenAppToken GetByUniqueKey(string userToken, string appId)
        {
            return AllInclude(x=> x.Token).SingleOrDefault(x=> 
                x.Aplicacao.AppId.Equals(appId, StringComparison.InvariantCultureIgnoreCase) &&
                x.Token.Hash.Equals(userToken, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
