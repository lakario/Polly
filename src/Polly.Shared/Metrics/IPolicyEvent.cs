using System.Collections.Generic;
using System.Text;

namespace Polly.Metrics
{
    /// <summary>
    /// A base type for an event emitted by a policy.
    /// </summary>
    public interface IPolicyEvent
    {
        /// <summary>
        /// The UTC timestamp of the event in ticks.
        /// </summary>
        long Ticks { get; }

        /// <summary>
        /// Policy-specific data captured at the time of the event.
        /// </summary>
        PolicyEventData EventData { get; }
    }
}
