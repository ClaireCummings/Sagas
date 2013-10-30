using LFM.Submissions.Contract.LandRegistry;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public interface IEdrsPoller<out T>
    {
        string MessageId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        T Response { get; }
    
//        EarlyCompletionReceived Response { get; }
        bool Poll();
    }
}
