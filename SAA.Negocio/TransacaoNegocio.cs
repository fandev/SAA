using Repository.Pattern.Ef6;
using Repository.Pattern.UnitOfWork;
using SAA.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Negocio
{
    public class TransacaoNegocio
    {
        #region Propriedades
        private IUnitOfWork unit;
        #endregion

        public TransacaoNegocio(ref IUnitOfWork unitOfWork)
        {
            this.unit = unitOfWork;
        }
        
        # region Consultas
        public IEnumerable<Acao> ListAllByHash(string appGUID, string hashTransaction)
        {
            return unit.Repository<Acao>().Query(x => x.Transacao.Hash.Equals("hashTransaction") && x.Transacao.Aplicacao.Guid.Equals(appGUID)).Include(x=> x.Transacao).Select();
        } 
        #endregion
    }
}
