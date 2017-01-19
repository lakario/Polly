namespace Polly.Shared.CircuitBreaker
{
    /// <summary>
    /// 
    /// </summary>
    public enum CircuitAction
    {
        /// <summary>
        /// 
        /// </summary>
        PreExecute,

        /// <summary>
        /// 
        /// </summary>
        PostExecute,

        /// <summary>
        /// 
        /// </summary>
        Reset,

        /// <summary>
        /// 
        /// </summary>
        Isolate
    }
}