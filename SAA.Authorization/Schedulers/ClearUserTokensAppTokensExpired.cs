using FluentScheduler;
using SAA.Negocio;
using SAA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAA.Authorization.Schedulers
{
    public class ClearUserTokensAppTokensExpired : ITask
    {
        private UnitOfWork unit = new UnitOfWork();
        private void Limpar()
        {
            try
            {
                var expiredUserTokenApp = TokenManager.AppUserTokens.Where(x => x.Value.Expiration < DateTime.Now);
                foreach (var item in expiredUserTokenApp)
                {
                    TokenManager.AppUserTokens.Remove(item.Key);
                }
                unit.UserTokenAppTokenRepository.DeleteRange(x => x.DataExpiracao < DateTime.Now);
                unit.SaveChanges();
            } catch(Exception){

            }
        }

        public void Execute()
        {
            Limpar();
        }
    }
}