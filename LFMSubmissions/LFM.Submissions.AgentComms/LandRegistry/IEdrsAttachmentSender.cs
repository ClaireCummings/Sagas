using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public interface IEdrsAttachmentSender : IEdrsPoller<IEdrsAttachmentResponseReceived>
    {
        string ApplicationMessageId { get; set; }
//        string MessageId { get; set; }
//        string Username { get; set; }
//        string Password { get; set; }
        string Payload { get; set; }
//        IEdrsAttachmentResponseReceived Response { get; }
//        bool Submit();
    }
}