using System;
using System.Reactive.Subjects;

namespace Polly.Metrics
{
    internal class EventsBroker : IEventsBroker
    {
        private readonly Subject<IPolicyEvent> _eventsSubject = new Subject<IPolicyEvent>();

        public bool Enabled { get; private set; }

        public IObservable<IPolicyEvent> Events => _eventsSubject;

        public virtual void OnActionPreExecute(PolicyData data, Context context)
        {
            if (Enabled)
                _eventsSubject.OnNext(new PolicyPreExecuteEvent(data, context));
        }

        public virtual void OnActionPostExecute(PolicyData data, Context context, OutcomeType outcomeType, Exception exception = null)
        {
            if (Enabled)
                _eventsSubject.OnNext(new PolicyPostExecuteEvent(data, context, outcomeType, exception));
        }

        public virtual void OnCustomEvent(PolicyData data, string eventType)
        {
            if (Enabled)
                _eventsSubject.OnNext(new PolicyCustomEvent(data, eventType));
        }

        public void Enable()
        {
            Enabled = true;
        }

        public void Disable()
        {
            if (!Enabled)
                return;

            _eventsSubject.OnCompleted();
        }
    }
}
