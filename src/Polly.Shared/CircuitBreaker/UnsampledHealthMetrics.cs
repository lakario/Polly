using Polly.Utilities;

namespace Polly.CircuitBreaker
{
    internal class UnsampledHealthMetrics : IHealthMetrics
    {
        protected HealthCount _current;

        public void IncrementSuccess_NeedsLock()
        {
            ActualiseCurrentMetric_NeedsLock();

            _current.Failures = 0;
            _current.Successes++;
        }

        public void IncrementFailure_NeedsLock()
        {
            ActualiseCurrentMetric_NeedsLock();

            _current.Failures++;
        }

        public void Reset_NeedsLock()
        {
            _current = null;
        }

        public IHealthCount GetHealthCount_NeedsLock()
        {
            ActualiseCurrentMetric_NeedsLock();

            return _current;
        }

        private void ActualiseCurrentMetric_NeedsLock()
        {
            long now = SystemClock.UtcNow().Ticks;
            if (_current == null)
            {
                _current = new HealthCount(now);
            }
        }
    }
}
