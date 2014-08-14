using FluentScheduler;
using SAA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAA.Authentication.Schedulers
{
    public class LimpaTokensExpirados : ITask
    {
        private UnitOfWork unit = new UnitOfWork();

        private void Limpar()
        {
            unit.TokenRepository.DeleteRange(x => !x.UserTokenAppTokens.Any());
            unit.SaveChanges();
        }
        
        public void Execute()
        {
            Limpar();
        }
    }
}