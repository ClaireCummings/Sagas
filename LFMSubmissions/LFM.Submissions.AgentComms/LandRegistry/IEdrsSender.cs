using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public interface IEdrsSender
    {
        string ApplicationId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Payload { get; set; }
        IEdrsResponseReceived Response { get;}

        bool Submit();
        bool Poll();
    }
}