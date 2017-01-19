using Polly.CircuitBreaker;

namespace Polly.Shared.CircuitBreaker
{
    internal class CircuitEvent : ICircuitEvent
    {
        public long Ticks { get; set; }
        public CircuitState State { get; set; }
        public CircuitAction Action { get; set; }
      
    }
}