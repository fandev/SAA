using FluentScheduler;
using SAA.Model.ViewModel;
using SAA.Negocio;
using SAA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAA.Authorization.Schedulers
{
    public class LoadUserTokensAppTokens : ITask
    {
        private UnitOfWork unit = new UnitOfWork();
        private void Load()
        {
            try{
            var userTokenAppToken = unit.UserTokenAppTokenRepository.AllInclude(x=> x.Aplicacao, y=> y.Token).Where(x => x.DataExpiracao > DateTime.Now);
            foreach (var item in userTokenAppToken)
            {
                var token = new AppUserToken
                {
                    AppId = item.Aplicacao.AppId,
                    UserToken = item.Token.Hash,
                    Expiration = item.DataExpiracao
                };

                TokenManager.AppUserTokens.Add(token.UserToken + token.AppId, token);
            }
            } catch(Exception) {

            }
        }

        public void Execute()
        {
            Load();
        }
    }
}