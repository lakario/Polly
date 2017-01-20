using System;
using System.Collections.Generic;
using System.Text;

namespace Polly.Metrics
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventSource
    {
        /// <summary>
        /// 
        /// </summary>
        IObservable<IPolicyEvent> PolicyEvents { get; }

        /// <summary>
        /// 
        /// </summary>
        void EnableMetrics();
    }
}
