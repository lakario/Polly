using System;

namespace Polly.Metrics
{
    /// <summary>
    /// An <see cref="IPolicyEvent"/> which occurs before or after action execution.
    /// </summary>
    public interface IActionExecutePolicyEvent : IPolicyEvent
    {
        /// <summary>
        /// A unique <see cref="Guid"/> which is generated for every action execution. May be used to group pre and post execution events.
        /// </summary>
        Guid ExecutionGuid { get; }

        /// <summary>
        /// The value provided to <see cref="Context.ExecutionKey"/> when the action was executed.
        /// </summary>
        string ExecutionKey { get; }
    }
}