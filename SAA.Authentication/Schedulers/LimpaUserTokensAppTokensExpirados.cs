using FluentScheduler;
using SAA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAA.Authentication.Schedulers
{
    public class LimpaUserTokensAppTokensExpirados : ITask
    {
        private UnitOfWork unit = new UnitOfWork();

        private void Limpar()
        {
            unit.UserTokenAppTokenRepository.DeleteRange(x => x.DataExpiracao < DateTime.Now);
            unit.SaveChanges();
        }
        
        public void Execute()
        {
            Limpar();
        }
    }
}