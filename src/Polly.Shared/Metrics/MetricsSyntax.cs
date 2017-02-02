using Polly.Metrics;

namespace Polly
{
    /// <summary>
    /// 
    /// </summary>
    public static class MetricsSyntax
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPolicy"></typeparam>
        /// <param name="metricsSource"></param>
        /// <returns></returns>
        public static TPolicy WithMetrics<TPolicy>(this TPolicy metricsSource)
            where TPolicy : IEventSource
        {
            metricsSource.EnableEvents();

            return metricsSource;
        }
    }
}