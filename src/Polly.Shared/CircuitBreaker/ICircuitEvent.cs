using System.Collections.Generic;
using System.Text;
using Polly.CircuitBreaker;

namespace Polly.Shared.CircuitBreaker
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICircuitEvent
    {
        /// <summary>
        /// 
        /// </summary>
        long Ticks { get; }

        /// <summary>
        /// 
        /// </summary>
        CircuitState State { get; }

        /// <summary>
        /// 
        /// </summary>
        CircuitAction Action { get; }

     
    }
}
