using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.BackgroundJobs.HangFire
{
    public class HangFireBackgroundWorker : IBackgroundWorker
    {
        public string ContinueWith(int jobId, Expression<Action> methodCall)
        {
            throw new NotImplementedException();
        }

        public string Enqueue(Expression<Action> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }

        public void Recurring(Expression<Action> methodCall, string cronExpression)
        {
             RecurringJob.AddOrUpdate(methodCall,cronExpression);
        }

        public string Schedule(Expression<Action> methodCall, TimeSpan delay)
        {
            return BackgroundJob.Schedule(methodCall, delay);
        }

    }
}
