using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentScheduler;
using SAA.Authentication.Schedulers;

namespace SAA.Authentication.Security
{
    public class SchedulerControl : Registry
    {
        /// <summary>
        /// Executa tarefas relacionadas com a segurança da WebAPI
        /// </summary>
        public SchedulerControl()
        {            
            // Remove tokens de usuário do cache para autmentar a peformance. Executa a cada 10 minutos
            Schedule<CarragarUserTokensTask>().ToRunNow().DelayFor(5).Seconds();
            Schedule<LimpaTokensExpirados>().ToRunNow().AndEvery(10).Minutes().DelayFor(1).Minutes();
            Schedule<LimpaUserTokensAppTokensExpirados>().ToRunNow().AndEvery(10).Minutes().DelayFor(1).Minutes();
        }
    }
}