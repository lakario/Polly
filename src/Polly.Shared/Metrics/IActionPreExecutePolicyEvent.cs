namespace Polly.Metrics
{
    /// <summary>
    /// An <see cref="IPolicyEvent"/> which occurs before action execution.
    /// </summary>
    public interface IActionPreExecutePolicyEvent : IActionExecutePolicyEvent
    {
    }
}