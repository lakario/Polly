using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Polly.Metrics
{
    internal class EventsBroker : IEventsBroker, IDisposable
    {
        private readonly Subject<IPolicyEvent> _eventsSubject = new Subject<IPolicyEvent>();

        public bool Enabled { get; private set; }

        public IObservable<IPolicyEvent> Events { get; }

        public EventsBroker()
        {
            Events = _eventsSubject.Publish().RefCount();
        }

        public virtual void OnActionPreExecute(Context context, PolicyEventData eventData = null)
        {
            if (Enabled)
                _eventsSubject.OnNext(new ActionPreExecutePolicyEvent(context, eventData));
        }

        public void OnActionSuccess(Context context, PolicyEventData eventData = null)
        {
            OnActionPostExecute(context, OutcomeType.Successful, null, eventData);
        }

        public void OnActionFailure(Context context, Exception exception, PolicyEventData eventData = null)
        {
            OnActionPostExecute(context, OutcomeType.Failure, exception, eventData);
        }

        public virtual void OnCustomEvent(string eventType, PolicyEventData eventData = null)
        {
            if (Enabled)
                _eventsSubject.OnNext(new CustomPolicyEvent(eventData, eventType));
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

        #region Helpers

        private void OnActionPostExecute(Context context, OutcomeType outcomeType, Exception exception, PolicyEventData eventData)
        {
            if (Enabled)
                _eventsSubject.OnNext(new ActionPostExecutePolicyEvent(context, outcomeType, exception, eventData));
        }

        public void Dispose()
        {
            _eventsSubject?.Dispose();
        }

        #endregion
    }
}
