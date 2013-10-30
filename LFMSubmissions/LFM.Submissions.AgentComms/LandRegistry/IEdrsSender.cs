using LFM.Submissions.InternalMessages.LandRegistry.Messages;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public interface IEdrsSender : IEdrsPoller<IEdrsResponseReceived>
    {
//        string MessageId { get; set; }
//        string Username { get; set; }
//        string Password { get; set; }
        string Payload { get; set; }
//        IEdrsResponseReceived Response { get;}
//        bool Submit();
    }
}