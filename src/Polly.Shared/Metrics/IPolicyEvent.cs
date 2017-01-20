using System;
using System.Collections.Generic;
using System.Text;

namespace Polly.Metrics
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPolicyEvent
    {
        /// <summary>
        /// 
        /// </summary>
        long Ticks { get; }

        /// <summary>
        /// 
        /// </summary>
        PolicyData Data { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPolicyExecuteEvent : IPolicyEvent
    {
        /// <summary>
        /// 
        /// </summary>
        Guid ExecutionGuid { get; }

        /// <summary>
        /// 
        /// </summary>
        string ExecutionKey { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPolicyPreExecuteEvent : IPolicyExecuteEvent
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPolicyPostExecuteEvent : IPolicyExecuteEvent
    {
        /// <summary>
        /// 
        /// </summary>
        OutcomeType OutcomeType { get; }

        /// <summary>
        /// 
        /// </summary>
        Exception Exception { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPolicyCustomEvent : IPolicyEvent
    {
        /// <summary>
        /// 
        /// </summary>
        string EventType { get; }
    }
}
