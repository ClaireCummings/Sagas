using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public interface IOutstandingRequestsPoller : IEdrsPoller<List<IOutstandingRequest>>
    {
//        string MessageId { get; set; }
//        string Username { get; set; }
//        string Password { get; set; }
//        List<IOutstandingRequest> Response { get; }
//        bool Poll();
    }
}