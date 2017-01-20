using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Polly.Shared.CircuitBreaker;
using Polly.Metrics;

namespace Polly.CircuitBreaker
{
    internal partial class CircuitBreakerEngine
    {
        internal static TResult Implementation<TResult>(
            Func<CancellationToken, TResult> action,
            Context context,
            CancellationToken cancellationToken,
            IEnumerable<ExceptionPredicate> shouldHandleExceptionPredicates, 
            IEnumerable<ResultPredicate<TResult>> shouldHandleResultPredicates, 
            ICircuitController<TResult> breakerController,
            IEventsBroker eventsBroker)
        {
            cancellationToken.ThrowIfCancellationRequested();

            eventsBroker?.OnActionPreExecute(new CircuitBreakerData(breakerController.CircuitState, breakerController.HealthCount), context);

            breakerController.OnActionPreExecute(context);

            try
            {
                DelegateResult<TResult> delegateOutcome = new DelegateResult<TResult>(action(cancellationToken));

                if (shouldHandleResultPredicates.Any(predicate => predicate(delegateOutcome.Result)))
                {
                    breakerController.OnActionFailure(delegateOutcome, context);

                    eventsBroker?.OnActionPostExecute(new CircuitBreakerData(breakerController.CircuitState, breakerController.HealthCount), context, OutcomeType.Failure, delegateOutcome.Exception);
                }
                else
                {
                    breakerController.OnActionSuccess(context);

                    eventsBroker?.OnActionPostExecute(new CircuitBreakerData(breakerController.CircuitState, breakerController.HealthCount), context, OutcomeType.Successful);
                }

                return delegateOutcome.Result;
            }
            catch (Exception ex)
            {
                eventsBroker?.OnActionPostExecute(new CircuitBreakerData(breakerController.CircuitState, breakerController.HealthCount), context, OutcomeType.Failure, ex);

                if (!shouldHandleExceptionPredicates.Any(predicate => predicate(ex)))
                {
                    throw;
                }

                breakerController.OnActionFailure(new DelegateResult<TResult>(ex), context);

                throw;
            }
        }
    }
}