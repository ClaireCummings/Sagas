using System;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;
using NServiceBus;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public class SubmitEdrsProcessor : IHandleMessages<SubmitEdrs>
    {
        public IBus Bus { get; set; }

        public void Handle(SubmitEdrs message)
        {
            var sender = new EdrsSender
                {
                    ApplicationId = message.ApplicationId,
                    Username = message.Username,
                    Password = message.Password,
                    Payload = message.Payload
                };
                        
            var response = sender.Submit();

            Console.WriteLine("GovGateway EdrsService Responded: " + response.GatewayResponse.Acknowledgement.MessageDescription);
            var responseMessage = EdrsResponseAnalyser.GetEdrsResponse(response);

            responseMessage.ApplicationId = message.ApplicationId;
            responseMessage.Username = message.Username;
            responseMessage.Password = message.Password;

            Bus.Reply(responseMessage);
        }
    }
}
