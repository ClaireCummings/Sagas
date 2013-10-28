namespace LFM.Submissions.AgentServices.LandRegistry
{
    public interface IEdrsSender
    {
        string ApplicationId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Payload { get; set; }

        EdrsSubmissionService.ResponseApplicationToChangeRegisterV1_0Type Submit();
        EdrsPollRequestService.ResponseApplicationToChangeRegisterV1_0Type Poll();
    }
}