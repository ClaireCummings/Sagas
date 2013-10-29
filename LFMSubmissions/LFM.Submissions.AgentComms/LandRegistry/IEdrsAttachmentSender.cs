using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public interface IEdrsAttachmentSender
    {
        string ApplicationId { get; set; }
        string AttachmentId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Payload { get; set; }
        IEdrsAttachmentResponseReceived Response { get; }
        bool Submit();
        bool Poll();
    }
}