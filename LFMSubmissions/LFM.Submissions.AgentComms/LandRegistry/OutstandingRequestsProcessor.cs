using System;
using System.Collections.Generic;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus;
using NServiceBus.Saga;
using LFM.Submissions.AgentComms.LandRegistry;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public class OutstandingRequestsProcessor : Saga<OutstandingRequestsSagaData>, IAmStartedByMessages<PollForOutstandingRequests>, IHandleMessages<GetOutstandingRequests>, IHandleMessages<StopPollingForOutstandingRequests>
    {
        public IEdrsPoller<List<IOutstandingRequest>> OutstandingRequestsPoller { get; set; }

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

            if (!Data.OngoingApplications.Contains(message.ApplicationId))
                Data.OngoingApplications.Add(message.ApplicationId);

            this.Data.Username = message.Username;

            if (!this.Data.IsPolling)
            {
                this.Data.IsPolling = true;
                // TODO Timespan configurable -- recommended 2 hours (using seconds for test)
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

            OutstandingRequestsPoller.MessageId = message.RequestId;
            OutstandingRequestsPoller.Username = message.Username;
            OutstandingRequestsPoller.Password = message.Password;
            
            if (OutstandingRequestsPoller.Poll())
            {
                foreach (var outstandingRequest in OutstandingRequestsPoller.Response)
                {
                    outstandingRequest.Username = message.Username;
                    outstandingRequest.Password = message.Password;
                    Bus.SendLocal(outstandingRequest);
                }
            }
            else
            {
                MarkAsComplete();              
            }
            
            // TODO:  Configure the polling interval - recommended is 2 hours
            Bus.Defer(TimeSpan.FromSeconds(20),
                      new GetOutstandingRequests {RequestId = Guid.NewGuid().ToString(), Username = message.Username, Password = message.Password});
        }

        public void Handle(StopPollingForOutstandingRequests message)
        {
            Data.OngoingApplications.Remove(message.ApplicationId);
            
            Console.WriteLine((string) "ApplicationId: {0} has been removed from the OngoingApplications list", (object) message.ApplicationId);
            if(Data.OngoingApplications.Count == 0)
                MarkAsComplete();
        }
    }
}