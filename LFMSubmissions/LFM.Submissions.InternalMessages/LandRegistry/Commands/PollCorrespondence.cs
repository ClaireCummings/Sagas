using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFM.Submissions.InternalMessages.LandRegistry.Commands
{
    public class PollCorrespondence : IOutstandingRequest
    {
        public string Username { get; set; }
        public string Password { get; set; } 
        public string MessageId { get; set; }
    }
}
