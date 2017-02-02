using System;
using Polly.Metrics;

namespace Polly
{
    internal class PolicyPipelineWithEvents<TResult> : PolicyPipeline<TResult>, IEventSource
    {
        private readonly IEventsBroker _eventsBroker;
        private readonly Func<PolicyEventData> _getPolicyEventDataFunc;

        public IObservable<IPolicyEvent> PolicyEvents => _eventsBroker.Events;

        public PolicyPipelineWithEvents(IEventsBroker eventsBroker)
            : this(eventsBroker, () => null)
        {
        }

        public PolicyPipelineWithEvents(IEventsBroker eventsBroker, Func<PolicyEventData> policyEventDataFunc)
        {
            if (eventsBroker == null) throw new ArgumentNullException(nameof(eventsBroker));

            _eventsBroker = eventsBroker;
            _getPolicyEventDataFunc = policyEventDataFunc;
        }

        public override void OnActionPreExecute(Context context)
        {
            _eventsBroker.OnActionPreExecute(context, _getPolicyEventDataFunc());
        }

        public override void OnActionSuccess(Context context)
        {
            _eventsBroker.OnActionSuccess(context, _getPolicyEventDataFunc());
        }

        public override void OnActionFailure(Context context, DelegateResult<TResult> result)
        {
            _eventsBroker.OnActionFailure(context, result.Exception, _getPolicyEventDataFunc());
        }

        public void EnableEvents()
        {
            _eventsBroker.Enable();
        }
    }
}