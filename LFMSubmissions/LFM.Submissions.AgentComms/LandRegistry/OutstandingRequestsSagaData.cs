using System;
using System.Collections.Generic;
using NServiceBus.Saga;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public class OutstandingRequestsSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public string ApplicationId { get; set; }
        public string Username { get; set; }

        public List<string> OngoingApplications { get; set; }
        
        public bool IsPolling { get; set; } 
    }
}
