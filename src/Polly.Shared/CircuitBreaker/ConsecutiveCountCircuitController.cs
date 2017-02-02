using System;
using Polly.CircuitBreaker;
using Polly.Utilities;

namespace Polly.CircuitBreaker
{
    internal class ConsecutiveCountCircuitController<TResult> : CircuitStateController<TResult>
    {
        private readonly int _exceptionsAllowedBeforeBreaking;
        private readonly IHealthMetrics _metrics;

        public ConsecutiveCountCircuitController(
            int exceptionsAllowedBeforeBreaking,
            TimeSpan durationOfBreak,
            Action<DelegateResult<TResult>, TimeSpan, Context> onBreak,
            Action<Context> onReset,
            Action onHalfOpen
            ) : base(durationOfBreak, onBreak, onReset, onHalfOpen)
        {
            _exceptionsAllowedBeforeBreaking = exceptionsAllowedBeforeBreaking;

            _metrics = new UnsampledHealthMetrics();
        }

        public override IHealthCount HealthCount
        {
            get
            {
                using (TimedLock.Lock(_lock))
                {
                    return _metrics.GetHealthCount_NeedsLock();
                }
            }
        }

        public override void OnCircuitReset(Context context)
        {
            using (TimedLock.Lock(_lock))
            {
                // Is only null during initialization of the current class
                // as the variable is not set, before the base class calls
                // current method from constructor.
                if (_metrics != null)
                    _metrics.Reset_NeedsLock();

                ResetInternal_NeedsLock(context);
            }
        }

        public override void OnActionSuccess(Context context)
        {
            using (TimedLock.Lock(_lock))
            {
                switch (_circuitState)
                {
                    case CircuitState.HalfOpen:
                        OnCircuitReset(context);
                        break;
                    case CircuitState.Closed:
                        _metrics.IncrementSuccess_NeedsLock();
                        break;
                }
            }
        }

        public override void OnActionFailure(Context context, DelegateResult<TResult> outcome)
        {
            using (TimedLock.Lock(_lock))
            {
                _lastOutcome = outcome;

                if (_circuitState == CircuitState.HalfOpen)
                {
                    Break_NeedsLock(context);
                    return;
                }

                _metrics.IncrementFailure_NeedsLock();

                var healthCount = _metrics.GetHealthCount_NeedsLock();

                if (healthCount.Failures >= _exceptionsAllowedBeforeBreaking)
                {
                    Break_NeedsLock(context);
                }
            }
        }
    }
}
