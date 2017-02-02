using System.Collections.Generic;
using System.Text;
using Polly.Utilities;

namespace Polly.Metrics
{
    internal class PolicyEvent : IPolicyEvent
    {
        public long Ticks { get; }

        public PolicyEventData EventData { get; }

        public PolicyEvent(PolicyEventData eventData = null)
        {
            Ticks = SystemClock.UtcNow().Ticks;
            EventData = eventData;
        }
    }
}
