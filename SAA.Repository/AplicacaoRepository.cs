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
    public class AplicacaoRepository : RepositoryBase<Aplicacao>
    {
        public AplicacaoRepository(DbContext context) : base(context) { }


        /// <summary>
        /// Recupera uma Aplicação pelo seu identificador único no formato Guid.Tostring("n")
        /// </summary>
        /// <param name="appId">Guid in format "N", Ex:. 00000000000000000000000000000000</param>
        /// <returns>Uma Aplicação</returns>
        public Aplicacao GetByAppId(string appId)
        {
            return AllInclude(x => x.TipoAplicacao, y=> y.AplicacaoUrls).SingleOrDefault(a => a.AppId.Equals(appId, StringComparison.InvariantCultureIgnoreCase));
        }

        public Aplicacao GetByAppKey(string appKey)
        {
            return AllInclude(x => x.TipoAplicacao).SingleOrDefault(a => a.AppKey.Equals(appKey, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool Existe(string appId, string appKey)
        {
            return All()
                .Any(x=> x.AppId.Equals(appId, StringComparison.InvariantCultureIgnoreCase) &&
                    x.AppKey.Equals(appKey));
        }
    }
}
