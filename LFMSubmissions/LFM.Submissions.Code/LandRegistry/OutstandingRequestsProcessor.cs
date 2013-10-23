using System;
using System.Collections.Generic;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus;
using NServiceBus.Saga;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public class OutstandingRequestsProcessor : Saga<OutstandingRequestsSagaData>, IAmStartedByMessages<PollForOutstandingRequests>, IHandleMessages<GetOutstandingRequests>, IHandleMessages<StopPollingForOutstandingRequests>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<PollForOutstandingRequests>(s => s.Username, m => m.Username);
            ConfigureMapping<GetOutstandingRequests>(s => s.Username, m => m.Username);
            ConfigureMapping<StopPollingForOutstandingRequests>(s => s.Username, m => m.Username);
        }

        public void Handle(PollForOutstandingRequests message)
        {
            if(this.Data.OngoingApplications == null)
                this.Data.OngoingApplications = new List<string>();

            Console.WriteLine("GovGateway received message PollForOutstandingRequests");
            this.Data.OngoingApplications.Add(message.ApplicationId);
            this.Data.Username = message.Username;

            if (!this.Data.IsPolling)
            {
                this.Data.IsPolling = true;
                // TODO Timespan configurable -- recommended 2 hours (using 2 seconds for test)
                Bus.Defer(TimeSpan.FromSeconds(30), new GetOutstandingRequests
                    {
                        RequestId = Guid.NewGuid().ToString(),
                        Username = message.Username,
                        Password = message.Password
                    });
                Console.WriteLine("Started polling for outstanding requests for user " + message.Username);
            }
        }

        public void Handle(GetOutstandingRequests message)
        {
            Console.WriteLine("About get Outstanding Requests");

            var sender = new OutstandingRequestsSender
                {
                    RequestId = message.RequestId, Username = message.Username, Password = message.Password
                };
            var response = sender.Query();
            
            if (OutstandingRequestsResponseAnalyser.WasRejected(response))
                MarkAsComplete();

            foreach (var outstandingRequest in OutstandingRequestsResponseAnalyser.GetOutstandingRequests(response))
            {
                if (this.Data.OngoingApplications.Contains(outstandingRequest.MessageId) || outstandingRequest.GetType() == typeof(PollCorrespondence))
                {
                    outstandingRequest.Username = message.Username;
                    outstandingRequest.Password = message.Password;
                    Bus.SendLocal(outstandingRequest);
                }
            }

            Bus.Defer(TimeSpan.FromSeconds(30),
                      new GetOutstandingRequests {RequestId = Guid.NewGuid().ToString(), Username = message.Username, Password = message.Password});
        }

        public void Handle(StopPollingForOutstandingRequests message)
        {
            this.Data.OngoingApplications.Remove(message.ApplicationId);
            Console.WriteLine("ApplicationId: {0} has been removed from the OngoingApplications list", message.ApplicationId);
            if(this.Data.OngoingApplications.Count == 0)
                MarkAsComplete();
        }
    }

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
