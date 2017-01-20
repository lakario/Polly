using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Reactive;
using System.Text;
using System.Threading;
using Polly.Metrics;
using Polly.Utilities;

namespace Polly
{
    public partial class Policy
    {
        internal readonly IEventsBroker EventsBroker;

        internal Policy(
            Action<Action<CancellationToken>, Context, CancellationToken> exceptionPolicy,
            IEnumerable<ExceptionPredicate> exceptionPredicates,
            IEventsBroker eventsBroker)
        {
            if (exceptionPolicy == null) throw new ArgumentNullException(nameof(exceptionPolicy));

            _exceptionPolicy = exceptionPolicy;
            _exceptionPredicates = exceptionPredicates ?? PredicateHelper.EmptyExceptionPredicates;

            EventsBroker = eventsBroker;
        }
    }

    public partial class Policy<TResult>
    {
        internal readonly IEventsBroker EventsBroker;

        internal Policy(
            Func<Func<CancellationToken, TResult>, Context, CancellationToken, TResult> executionPolicy,
            IEnumerable<ExceptionPredicate> exceptionPredicates,
            IEnumerable<ResultPredicate<TResult>> resultPredicates,
            IEventsBroker eventsBroker)
        {
            if (executionPolicy == null) throw new ArgumentNullException("executionPolicy");

            _executionPolicy = executionPolicy;
            _exceptionPredicates = exceptionPredicates ?? PredicateHelper.EmptyExceptionPredicates;
            _resultPredicates = resultPredicates ?? PredicateHelper<TResult>.EmptyResultPredicates;

            EventsBroker = eventsBroker;
        }
    }
}
