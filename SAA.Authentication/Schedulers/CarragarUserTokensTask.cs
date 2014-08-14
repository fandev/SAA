using FluentScheduler;
using SAA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAA.Negocio;

namespace SAA.Authentication.Schedulers
{
    public class CarragarUserTokensTask : ITask
    {
        private UnitOfWork unit = new UnitOfWork();

        private void Carregar()
        {
            var storedTokens = unit.TokenRepository.All();
            if (storedTokens.Count() > 0)
                TokenManager.ListTokens.Concat(storedTokens.ToList());
        }

        public void Execute()
        {
            Carregar();
        }
    }
}