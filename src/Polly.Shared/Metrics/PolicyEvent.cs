using System;
using System.Collections.Generic;
using System.Text;
using Polly.Utilities;

namespace Polly.Metrics
{
    internal class PolicyEvent : IPolicyEvent
    {
        public long Ticks { get; }

        public PolicyData Data { get; }

        public PolicyEvent(PolicyData data)
        {
            Ticks = SystemClock.UtcNow().Ticks;
            Data = data;
        }
    }

    internal abstract class PolicyExecuteEvent : PolicyEvent, IPolicyExecuteEvent
    {
        public Guid ExecutionGuid { get; }

        public string ExecutionKey { get; }

        protected PolicyExecuteEvent(PolicyData data, Context context)
            : base(data)
        {
            ExecutionGuid = context.ExecutionGuid;
            ExecutionKey = context.ExecutionKey;
        }

    }

    internal class PolicyPreExecuteEvent : PolicyExecuteEvent, IPolicyPreExecuteEvent
    {
      
        public PolicyPreExecuteEvent(PolicyData data, Context context) : base(data, context)
        {
        }
    }

    internal class PolicyPostExecuteEvent : PolicyExecuteEvent, IPolicyPostExecuteEvent
    {
        public OutcomeType OutcomeType { get; }

        public Exception Exception { get; }

        public PolicyPostExecuteEvent(PolicyData data, Context context, OutcomeType outcomeType, Exception exception) : base(data, context)
        {
            OutcomeType = outcomeType;
            Exception = exception;
        }

    }

    internal class PolicyCustomEvent : PolicyEvent, IPolicyCustomEvent
    {
        public string EventType { get; }

        public PolicyCustomEvent(PolicyData data, string eventType) : base(data)
        {
            EventType = eventType;
        }

    }
}
