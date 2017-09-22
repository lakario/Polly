using System;
using System.Collections.Generic;
using System.Text;

namespace Polly.Metrics
{
    /// <summary>
    /// A base type which policies can extend to provide additional context when emitting events.
    /// </summary>
    public abstract class PolicyEventData
    {
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, object> Data { get; } = new Dictionary<string, object>();
    }
}
