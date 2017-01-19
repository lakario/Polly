using System;

namespace Polly.Shared.CircuitBreaker
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICircuitExecuteEvent : ICircuitEvent
    {

        /// <summary>
        /// 
        /// </summary>
        string ExecutionKey { get; }

        /// <summary>
        /// 
        /// </summary>
        Guid ExecutionGuid { get; }

        /// <summary>
        /// 
        /// </summary>
        OutcomeType? OutcomeType { get; }

        /// <summary>
        /// 
        /// </summary>
        Exception Exception { get; }
    }
}