

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Polly.CircuitBreaker
{
    internal partial class CircuitBreakerEngine
    {
        internal static async Task<TResult> ImplementationAsync<TResult>(
            Func<CancellationToken, Task<TResult>> action, 
            Context context,
            IEnumerable<ExceptionPredicate> shouldHandleExceptionPredicates, 
            IEnumerable<ResultPredicate<TResult>> shouldHandleResultPredicates,
            ICircuitController<TResult> breakerController,
            CancellationToken cancellationToken, 
            bool continueOnCapturedContext)
        {
            cancellationToken.ThrowIfCancellationRequested();

            breakerController.OnActionPreExecute(context);

            try
            {
                DelegateResult<TResult> delegateOutcome = new DelegateResult<TResult>(await action(cancellationToken).ConfigureAwait(continueOnCapturedContext));

                if (shouldHandleResultPredicates.Any(predicate => predicate(delegateOutcome.Result)))
                {
                    breakerController.OnActionFailure(context, delegateOutcome);
                }
                else
                {
                    breakerController.OnActionSuccess(context);
                }

                return delegateOutcome.Result;
            }
            catch (Exception ex)
            {
                if (!shouldHandleExceptionPredicates.Any(predicate => predicate(ex)))
                {
                    throw;
                }

                breakerController.OnActionFailure(context, new DelegateResult<TResult>(ex));

                throw;
            }

        }

    }
}

