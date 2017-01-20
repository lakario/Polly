using System;
using Polly.Metrics;
using Polly.Shared.CircuitBreaker;

namespace Polly.CircuitBreaker
{
    internal interface ICircuitController<TResult>
    {
        IHealthCount HealthCount { get; }
        CircuitState CircuitState { get; }
        Exception LastException { get; }
        TResult LastHandledResult { get; }
        void Isolate();
        void Reset();
        void OnCircuitReset(Context context);
        void OnActionPreExecute(Context context);
        void OnActionSuccess(Context context);
        void OnActionFailure(DelegateResult<TResult> outcome, Context context);
    }
}
