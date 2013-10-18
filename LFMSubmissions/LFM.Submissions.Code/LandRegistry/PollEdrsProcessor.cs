using System;
using LFM.Submissions.GovGateway.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;
using NServiceBus;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public class PollEdrsProcessor : IHandleMessages<PollEdrs>
    {
        public IBus Bus { get; set; }

        public void Handle(PollEdrs message)
        {
            var sender = new EdrsSender
            {
                ApplicationId = message.ApplicationId,
                Username = message.Username,
                Password = message.Password,
            };

            var response = sender.Poll();

            var responseResult = EdrsResponseAnalyser.GetEdrsResponse(response);
            Console.WriteLine("GovGateway EdrsPollService Responded: " + response.GatewayResponse.Results.MessageDetails);
            Bus.Reply(new EdrsAcknowledgementReceived
            {
                ApplicationId = message.ApplicationId,
                Username = message.Username,
                Password = message.Password
            });
        }
    }
}
