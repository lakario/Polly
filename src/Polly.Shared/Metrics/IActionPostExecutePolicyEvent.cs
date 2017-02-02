using System;

namespace Polly.Metrics
{
    /// <summary>
    /// An <see cref="IPolicyEvent"/> which occurs after action execution.
    /// </summary>
    public interface IActionPostExecutePolicyEvent : IActionExecutePolicyEvent
    {
        /// <summary>
        /// The outcome type of the executed action.
        /// </summary>
        OutcomeType OutcomeType { get; }

        /// <summary>
        /// The exception thrown by the executed action.
        /// </summary>
        Exception Exception { get; }
    }
}