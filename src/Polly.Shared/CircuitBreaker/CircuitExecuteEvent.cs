using System;

namespace Polly.Shared.CircuitBreaker
{
    internal class CircuitExecuteEvent : CircuitEvent, ICircuitExecuteEvent
    {
        public OutcomeType? OutcomeType { get; set; }
        public Exception Exception { get; set; }
        public Guid? ExecutionGuid { get; set; }
        public string ExecutionKey { get; set; }
    }
}