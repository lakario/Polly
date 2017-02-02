using System;
using System.Collections.Generic;
using System.Text;
using Polly.Metrics;

namespace Polly
{
    /// <summary>
    /// TODO: Rename this....
    /// </summary>
    public interface IPolicyPipeline<TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void OnActionPreExecute(Context context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void OnActionSuccess(Context context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        void OnActionFailure(Context context, DelegateResult<TResult> result);
    }
}
