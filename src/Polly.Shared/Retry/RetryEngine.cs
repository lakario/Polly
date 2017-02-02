using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Polly.Retry
{
    internal static partial class RetryEngine
    {
        internal static TResult Implementation<TResult>(
            Func<CancellationToken, TResult> action, 
            Context context, 
            CancellationToken cancellationToken, 
            IEnumerable<ExceptionPredicate> shouldRetryExceptionPredicates, 
            IEnumerable<ResultPredicate<TResult>> shouldRetryResultPredicates, 
            Func<IRetryPolicyState<TResult>> policyStateFactory, 
            IPolicyPipeline<TResult> policyPipeline)
        {
            IRetryPolicyState<TResult> policyState = policyStateFactory();

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                policyPipeline.OnActionPreExecute(context);

                try
                {
                    DelegateResult<TResult> delegateOutcome = new DelegateResult<TResult>(action(cancellationToken));

                    if (!shouldRetryResultPredicates.Any(predicate => predicate(delegateOutcome.Result)))
                    {
                        policyPipeline.OnActionFailure(context, delegateOutcome);

                        return delegateOutcome.Result;
                    }

                    if (!policyState.CanRetry(delegateOutcome, cancellationToken))
                    {
                        policyPipeline.OnActionSuccess(context);

                        return delegateOutcome.Result;
                    }
                }
                catch (Exception ex)
                {
                    policyPipeline.OnActionFailure(context, new DelegateResult<TResult>(ex));

                    if (!shouldRetryExceptionPredicates.Any(predicate => predicate(ex)))
                    {
                        throw;
                    }

                    if (!policyState.CanRetry(new DelegateResult<TResult>(ex), cancellationToken))
                    {
                        throw;
                    }
                }
            }
        }
    }
}
