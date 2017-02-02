using System;
using Polly.Metrics;

namespace Polly.Metrics
{
    internal interface IEventsBroker
    {
        bool Enabled { get; }
        IObservable<IPolicyEvent> Events { get; }
        void OnActionPreExecute(Context context, PolicyEventData eventData = null);
        void OnActionSuccess(Context context, PolicyEventData eventData = null);
        void OnActionFailure(Context context, Exception exception, PolicyEventData eventData = null);
        void OnCustomEvent(string eventType, PolicyEventData eventData = null);
        void Enable();
        void Disable();
    }
}