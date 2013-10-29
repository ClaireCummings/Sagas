using System.Collections.Generic;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public interface IOutstandingRequestsPoller
    {
        string RequestId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        List<IOutstandingRequest> Response { get; }
        bool Poll();
    }
}
