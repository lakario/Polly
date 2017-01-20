using System;
using Polly.Metrics;

namespace Polly.Metrics
{
    internal interface IEventsBroker
    {
        bool Enabled { get; }
        IObservable<IPolicyEvent> Events { get; }
        void OnActionPreExecute(PolicyData data, Context context);
        void OnActionPostExecute(PolicyData data, Context context, OutcomeType outcomeType, Exception exception = null);
        void OnCustomEvent(PolicyData data, string eventType);
        void Enable();
        void Disable();
    }
}