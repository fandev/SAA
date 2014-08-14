using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAA.Authentication.Schedulers
{
    public class FluentSchedulerTaskFactory : ITaskFactory
    {
        public ITask GetTaskInstance<T>() where T : ITask
        {
            return Activator.CreateInstance<T>();
        }
    }
}