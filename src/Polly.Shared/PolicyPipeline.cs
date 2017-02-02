using System.Collections.Generic;
using System.Text;

namespace Polly
{
    internal abstract class PolicyPipeline<TResult> : IPolicyPipeline<TResult>
    {
        public abstract void OnActionPreExecute(Context context);

        public abstract void OnActionSuccess(Context context);

        public abstract void OnActionFailure(Context context, DelegateResult<TResult> result);
    }
}
