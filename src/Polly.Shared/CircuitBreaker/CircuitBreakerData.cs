using Polly.CircuitBreaker;
using Polly.Metrics;

namespace Polly.Shared.CircuitBreaker
{
    internal class CircuitBreakerData : PolicyData
    {
        public CircuitState State { get; set; }

        public IHealthCount HealthCount { get; set; }

        public CircuitBreakerData(CircuitState state, IHealthCount healthCount)
        {
            State = state;
            HealthCount = healthCount;
        }
    }
}
