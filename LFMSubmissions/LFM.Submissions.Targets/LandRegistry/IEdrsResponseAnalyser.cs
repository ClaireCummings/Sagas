using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentServices.LandRegistry
{
    public interface IEdrsResponseAnalyser
    {
        IEdrsResponseReceived GetEdrsResponse(EdrsSubmissionService.ResponseApplicationToChangeRegisterV1_0Type response);
        IEdrsResponseReceived GetEdrsResponse(EdrsPollRequestService.ResponseApplicationToChangeRegisterV1_0Type response);
        IEdrsAttachmentResponseReceived GetEdrsResponse(EdrsAttachmentService.AttachmentResponseV1_0Type response);
        IEdrsAttachmentResponseReceived GetEdrsResponse(EdrsAttachmentPollService.AttachmentResponseV1_0Type response);
    }
}