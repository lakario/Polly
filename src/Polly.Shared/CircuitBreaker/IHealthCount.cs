namespace Polly.CircuitBreaker
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHealthCount
    {
        /// <summary>
        /// 
        /// </summary>
        int Successes { get; }
        
        /// <summary>
        /// 
        /// </summary>
        int Failures { get; }

        /// <summary>
        /// 
        /// </summary>
        int Total { get; }

        /// <summary>
        /// 
        /// </summary>
        long StartedAt { get; }
    }
}