using System;

namespace Polly.Metrics
{
    internal class ActionPostExecutePolicyEvent : ActionExecutePolicyEvent, IActionPostExecutePolicyEvent
    {
        public OutcomeType OutcomeType { get; }

        public Exception Exception { get; }

        public ActionPostExecutePolicyEvent(Context context, OutcomeType outcomeType, Exception exception = null, PolicyEventData eventData = null) : base(context, eventData)
        {
            OutcomeType = outcomeType;
            Exception = exception;
        }
    }
}