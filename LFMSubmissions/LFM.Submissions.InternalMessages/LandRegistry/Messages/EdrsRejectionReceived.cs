﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFM.Submissions.InternalMessages.LandRegistry.Messages
{
    public class EdrsRejectionReceived : IEdrsResponseReceived
    {
        public string ApplicationId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RejectionReason { get; set; }
    }
}
