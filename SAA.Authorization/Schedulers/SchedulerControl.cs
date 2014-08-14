using FluentScheduler;
using SAA.Authorization.Schedulers;

namespace SAA.Authorization.Security
{
    public class SchedulerControl : Registry
    {
        /// <summary>
        /// Executa tarefas relacionadas com a segurança da WebAPI
        /// </summary>
        public SchedulerControl()
        {            
            // Remove tokens de usuário do cache para autmentar a peformance. Executa a cada 10 minutos
            Schedule<LoadUserTokensTask>().ToRunNow().DelayFor(5).Seconds();
            Schedule<LoadUserTokensAppTokens>().ToRunNow().DelayFor(5).Seconds();

            Schedule<ClearUserTokensExpiredTask>().ToRunNow().AndEvery(10).Minutes().DelayFor(1).Minutes();
            Schedule<ClearUserTokensAppTokensExpired>().ToRunNow().AndEvery(10).Minutes().DelayFor(1).Minutes();
        }
    }
}