using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.BackgroundJobs
{
    public interface IBackgroundWorker
    {
        string Schedule(Expression<Action> methodCall, TimeSpan delay);
        string Enqueue(Expression<Action> methodCall);
        void Recurring(Expression<Action> methodCall,string cronExpression);
        string ContinueWith(int jobId, Expression<Action> methodCall);
    }
}
