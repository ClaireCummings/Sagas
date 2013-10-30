using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public interface IEdrsAttachmentSender : IEdrsPoller<IEdrsAttachmentResponseReceived>
    {
        string ApplicationMessageId { get; set; }
        string Payload { get; set; }
    }
}