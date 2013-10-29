using LFM.Submissions.Contract.LandRegistry;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public interface IEdrsEarlyCompletionPoller
    {
        string MessageId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        EarlyCompletionReceived Response { get; }
        bool Poll();
    }
}
