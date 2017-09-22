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

            Data.Add($"CircuitBreaker_State", state.ToString());
            Data.Add($"CircuitBreaker_HealthCount_{nameof(IHealthCount.Successes)}", healthCount.Successes);
            Data.Add($"CircuitBreaker_HealthCount_{nameof(IHealthCount.Failures)}", healthCount.Failures);
            Data.Add($"CircuitBreaker_HealthCount_{nameof(IHealthCount.StartedAt)}", healthCount.StartedAt);
            Data.Add($"CircuitBreaker_HealthCount_{nameof(IHealthCount.Total)}", healthCount.Total);
        }
    }
}
