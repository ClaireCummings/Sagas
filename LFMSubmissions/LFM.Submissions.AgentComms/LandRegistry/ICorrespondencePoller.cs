using LFM.Submissions.Contract.LandRegistry;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public interface ICorrespondencePoller
    {
        string MessageId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        CorrespondenceReceived Response { get; }
        bool Poll();
    }
}
