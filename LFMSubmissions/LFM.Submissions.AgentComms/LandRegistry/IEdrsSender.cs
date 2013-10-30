using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public interface IEdrsSender<out T> : IEdrsPoller<T>
    {
        string Payload { get; set; }
    }
}