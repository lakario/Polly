using System;
using System.Collections.Generic;
using Polly.Utilities;
using System.Threading;
using Polly.Shared.CircuitBreaker;
using Polly.Metrics;

namespace Polly.CircuitBreaker
{
    /// <summary>
    /// A circuit-breaker policy that can be applied to delegates.
    /// </summary>
    public partial class CircuitBreakerPolicy : Policy, IEventSource
    {
        private readonly ICircuitController<EmptyStruct> _breakerController;

        internal CircuitBreakerPolicy(
            Action<Action<CancellationToken>, Context, CancellationToken> exceptionPolicy,
            IEnumerable<ExceptionPredicate> exceptionPredicates,
            ICircuitController<EmptyStruct> breakerController,
            IEventsBroker eventsBroker
            ) : base(exceptionPolicy, exceptionPredicates, eventsBroker)
        {
            _breakerController = breakerController;
        }

        /// <summary>
        /// 
        /// </summary>
        public IObservable<IPolicyEvent> PolicyEvents => EventsBroker.Events;

        /// <summary>
        /// Gets the state of the underlying circuit.
        /// </summary>
        public CircuitState CircuitState
        {
            get { return _breakerController.CircuitState; }
        }

        /// <summary>
        /// Gets the last exception handled by the circuit-breaker.
        /// </summary>
        public Exception LastException
        {
            get { return _breakerController.LastException; }
        }

        /// <summary>
        /// Isolates (opens) the circuit manually, and holds it in this state until a call to <see cref="Reset()"/> is made.
        /// </summary>
        public void Isolate()
        {
            _breakerController.Isolate();

            EventsBroker.OnCustomEvent(new CircuitData(CircuitState), nameof(Isolate));
        }

        /// <summary>
        /// Closes the circuit, and resets any statistics controlling automated circuit-breaking.
        /// </summary>
        public void Reset()
        {
            _breakerController.Reset();

            EventsBroker.OnCustomEvent(new CircuitData(CircuitState), nameof(Reset));
        }

        void IEventSource.EnableMetrics()
        {
            EventsBroker.Enable();
        }
    }

    /// <summary>
    /// A circuit-breaker policy that can be applied to delegates returning a value of type <typeparam name="TResult"/>.
    /// </summary>
    public partial class CircuitBreakerPolicy<TResult> : Policy<TResult>, IEventSource
    {
        private readonly ICircuitController<TResult> _breakerController;

        internal CircuitBreakerPolicy(
            Func<Func<CancellationToken, TResult>, Context, CancellationToken, TResult> executionPolicy,
            IEnumerable<ExceptionPredicate> exceptionPredicates,
            IEnumerable<ResultPredicate<TResult>> resultPredicates,
            ICircuitController<TResult> breakerController,
            IEventsBroker eventsBroker
            ) : base(executionPolicy, exceptionPredicates, resultPredicates, eventsBroker)
        {
            _breakerController = breakerController;
        }

        /// <summary>
        /// 
        /// </summary>
        public IObservable<IPolicyEvent> PolicyEvents => EventsBroker.Events;

        /// <summary>
        /// Gets the state of the underlying circuit.
        /// </summary>
        public CircuitState CircuitState
        {
            get { return _breakerController.CircuitState; }
        }

        /// <summary>
        /// Gets the last exception handled by the circuit-breaker.
        /// </summary>
        public Exception LastException
        {
            get { return _breakerController.LastException; }
        }

        /// <summary>
        /// Gets the last result returned from a user delegate which the circuit-breaker handled.
        /// </summary>
        public TResult LastHandledResult
        {
            get { return _breakerController.LastHandledResult; }
        }

        /// <summary>
        /// Isolates (opens) the circuit manually, and holds it in this state until a call to <see cref="Reset()"/> is made.
        /// </summary>
        public void Isolate()
        {
            _breakerController.Isolate();
        }

        /// <summary>
        /// Closes the circuit, and resets any statistics controlling automated circuit-breaking.
        /// </summary>
        public void Reset()
        {
            _breakerController.Reset();
        }

        void IEventSource.EnableMetrics()
        {
            EventsBroker.Enable();
        }
    }
}
