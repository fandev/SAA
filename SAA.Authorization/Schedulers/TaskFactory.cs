using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAA.Authorization.Schedulers
{
    public class FluentSchedulerTaskFactory : ITaskFactory
    {
        public ITask GetTaskInstance<T>() where T : ITask
        {
            return Activator.CreateInstance<T>();
        }
    }
}