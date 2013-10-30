using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public interface IEdrsSender : IEdrsPoller<IEdrsResponseReceived>
    {
        string Payload { get; set; }
    }
}