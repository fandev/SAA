using FluentScheduler;
using SAA.Authentication.Schedulers;
using SAA.Authentication.Security;
using SAA.Model.ViewModel;
using SAA.Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SAA.Authentication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            // Iniciar Lista de Tokens
            TokenManager.ListTokens = new System.Collections.Generic.List<Model.Models.Token>();
            TokenManager.AppUserTokens = new Dictionary<string, AppUserToken>();

            // Carrega lista de tokens de usuários para manter a sessão em caso de reinicio do aplicativo ou servidor   
            TaskManager.TaskFactory = new FluentSchedulerTaskFactory();
            TaskManager.UnobservedTaskException += TaskManager_UnobservedTaskException;
            //TaskManager.TaskStart += (schedule, ev) => file.Concatenar("\r\n" + schedule.Name + " Iniciou em: " + schedule.StartTime);
            //TaskManager.TaskEnd += (schedule, ev) => file.Concatenar(schedule.Name + " Finalizada.\n\tIniciou em: " + schedule.StartTime + "\n\tDuração: " + schedule.Duration + "\n\tProxima Execução: " + schedule.NextRunTime);

            TaskManager.Initialize(new SchedulerControl());
        }

        void TaskManager_UnobservedTaskException(FluentScheduler.Model.TaskExceptionInformation sender, System.UnhandledExceptionEventArgs e)
        {
            EventLog.WriteEntry("FluentScheduler",
                string.Format("Ocorreu uma excessão não tratada. \n  Tarefa {0}\n    Exception: {1}",
                sender.Name, (e.ExceptionObject as Exception).ToString()), EventLogEntryType.Error);
        }

        public override void Init()
        {
            base.PreSendRequestHeaders += WebApiApplication_PreSendRequestHeaders;
            base.Init();
        }

        /// <summary>
        /// Remover Headers Indesejados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebApiApplication_PreSendRequestHeaders(object sender, System.EventArgs e)
        {
            Context.Response.Headers.Remove("Server");
        }

    }
}
