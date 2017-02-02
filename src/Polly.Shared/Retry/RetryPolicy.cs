using System;
using System.Collections.Generic;
using System.Threading;
using Polly.Metrics;

namespace Polly.Retry
{
    /// <summary>
    /// A retry policy that can be applied to delegates.
    /// </summary>
    public partial class RetryPolicy : Policy, IEventSource
    {
        private readonly IEventsBroker _eventsBroker;
        internal RetryPolicy(Action<Action<CancellationToken>, Context, CancellationToken> exceptionPolicy, IEnumerable<ExceptionPredicate> exceptionPredicates, IEventsBroker eventsBroker) 
            : base(exceptionPolicy, exceptionPredicates)
        {
            _eventsBroker = eventsBroker;
        }

        /// <summary>
        /// 
        /// </summary>
        public IObservable<IPolicyEvent> PolicyEvents => _eventsBroker.Events;

        /// <summary>
        /// 
        /// </summary>
        public void EnableEvents()
        {
            _eventsBroker.Enable();
        }
    }

    /// <summary>
    /// A retry policy that can be applied to delegates returning a value of type <typeparam name="TResult"/>.
    /// </summary>
    public partial class RetryPolicy<TResult> : Policy<TResult>, IEventSource
    {
        private readonly IEventsBroker _eventsBroker;

        internal RetryPolicy(Func<Func<CancellationToken, TResult>, Context, CancellationToken, TResult> executionPolicy, IEnumerable<ExceptionPredicate> exceptionPredicates, IEnumerable<ResultPredicate<TResult>> resultPredicates, IEventsBroker eventsBroker) 
            : base(executionPolicy, exceptionPredicates, resultPredicates)
        {
            _eventsBroker = eventsBroker;
        }

        /// <summary>
        /// 
        /// </summary>
        public IObservable<IPolicyEvent> PolicyEvents => _eventsBroker.Events;

        /// <summary>
        /// 
        /// </summary>
        public void EnableEvents()
        {
            _eventsBroker.Enable();
        }
    }
}