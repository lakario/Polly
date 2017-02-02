namespace Polly.Metrics
{
    /// <summary>
    /// A special <see cref="IPolicyEvent"/> which is policy type specific.
    /// </summary>
    public interface ICustomPolicyEvent : IPolicyEvent
    {
        /// <summary>
        /// The custom event type.
        /// </summary>
        string EventType { get; }
    }
}