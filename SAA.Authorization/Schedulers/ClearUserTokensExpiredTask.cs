using FluentScheduler;
using SAA.Negocio;
using SAA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAA.Authorization.Schedulers
{
    public class ClearUserTokensExpiredTask : ITask
    {
        private UnitOfWork unit = new UnitOfWork();

        private void Limpar()
        {
            TokenManager.ListTokens.RemoveAll(x => TokenManager.AppUserTokens.Any(y => y.Value.UserToken.Equals(x.Hash)));
            unit.TokenRepository.DeleteRange(x => !x.UserTokenAppTokens.Any());
            unit.SaveChanges();
        }
        
        public void Execute()
        {
            Limpar();
        }
    }
}