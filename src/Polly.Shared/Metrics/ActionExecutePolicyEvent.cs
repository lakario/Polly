using System;

namespace Polly.Metrics
{
    internal abstract class ActionExecutePolicyEvent : PolicyEvent, IActionExecutePolicyEvent
    {
        public Guid ExecutionGuid { get; }

        public string ExecutionKey { get; }

        protected ActionExecutePolicyEvent(Context context, PolicyEventData eventData = null)
            : base(eventData)
        {
            ExecutionGuid = context.ExecutionGuid;
            ExecutionKey = context.ExecutionKey;
        }
    }
}