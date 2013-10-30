using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public interface IEdrsAttachmentSender : IEdrsSender<IEdrsAttachmentResponseReceived>
    {
        string ApplicationMessageId { get; set; }
    }
}