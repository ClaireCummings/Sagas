using EdrsService = LFM.Submissions.GovGateway.EdrsSubmissionService;
using EdrsPollService = LFM.Submissions.GovGateway.EdrsPollRequestService;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public interface IEdrsSender
    {
        string ApplicationId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Payload { get; set; }

        EdrsService.ResponseApplicationToChangeRegisterV1_0Type Submit();
        EdrsPollService.ResponseApplicationToChangeRegisterV1_0Type Poll();
    }
}