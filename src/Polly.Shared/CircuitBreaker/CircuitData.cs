using Polly.CircuitBreaker;
using Polly.Metrics;

namespace Polly.Shared.CircuitBreaker
{
    internal class CircuitData : PolicyData
    {
        public CircuitState State { get; set; }

        public CircuitData(CircuitState state)
        {
            State = state;
        }
    }
}
