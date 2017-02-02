using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Polly.CircuitBreaker;
using Polly.Metrics;
using Polly;

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
            IPolicyPipeline<TResult> policyPipeline)
        {
            cancellationToken.ThrowIfCancellationRequested();

            breakerController.OnActionPreExecute(context);

            policyPipeline.OnActionPreExecute(context);

            try
            {
                DelegateResult<TResult> delegateOutcome = new DelegateResult<TResult>(action(cancellationToken));

                if (shouldHandleResultPredicates.Any(predicate => predicate(delegateOutcome.Result)))
                {
                    policyPipeline?.OnActionFailure(context, delegateOutcome);

                    breakerController.OnActionFailure(context, delegateOutcome);
                }
                else
                {
                    policyPipeline?.OnActionSuccess(context);

                    breakerController.OnActionSuccess(context);
                }

                return delegateOutcome.Result;
            }
            catch (Exception ex)
            {
                var result = new DelegateResult<TResult>(ex);

                policyPipeline?.OnActionFailure(context, result);

                if (!shouldHandleExceptionPredicates.Any(predicate => predicate(ex)))
                {
                    throw;
                }

                breakerController.OnActionFailure(context, result);

                throw;
            }
        }
    }
}