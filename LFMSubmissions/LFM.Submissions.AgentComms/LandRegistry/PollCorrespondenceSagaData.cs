using System;
using NServiceBus.Saga;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public class PollCorrespondenceSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public string Username { get; set; }
  }
}
