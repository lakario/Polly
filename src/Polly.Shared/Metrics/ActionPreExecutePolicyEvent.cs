namespace Polly.Metrics
{
    internal class ActionPreExecutePolicyEvent : ActionExecutePolicyEvent, IActionPreExecutePolicyEvent
    {
        public ActionPreExecutePolicyEvent(Context context, PolicyEventData eventData = null) : base(context, eventData)
        {
        }
    }
}