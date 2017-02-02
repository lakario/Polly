namespace Polly.Metrics
{
    internal class CustomPolicyEvent : PolicyEvent, ICustomPolicyEvent
    {
        public string EventType { get; }

        public CustomPolicyEvent(PolicyEventData eventData, string eventType) : base(eventData)
        {
            EventType = eventType;
        }

    }
}