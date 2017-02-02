using Polly.CircuitBreaker;
using Polly.Metrics;

namespace Polly.CircuitBreaker
{
    internal class CircuitBreakerEventData : PolicyEventData
    {
        public CircuitState State { get; set; }

        public IHealthCount HealthCount { get; set; }

        public CircuitBreakerEventData(CircuitState state, IHealthCount healthCount)
        {
            State = state;
            HealthCount = healthCount;
        }
    }
}
