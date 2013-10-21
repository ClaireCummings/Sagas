using System;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
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
            var responseDescription = "No Description";

            if(response.GatewayResponse.Acknowledgement != null)
                responseDescription = response.GatewayResponse.Acknowledgement.MessageDescription;
            else if (response.GatewayResponse.Results != null)
                responseDescription = response.GatewayResponse.Results.MessageDetails;
            else if (response.GatewayResponse.Rejection.RejectionResponse != null)
                responseDescription = response.GatewayResponse.Rejection.RejectionResponse.Reason;

            Console.WriteLine("GovGateway EdrsPollService Responded: " + responseDescription);
            
            var responseMessage = EdrsResponseAnalyser.GetEdrsResponse(response);

            responseMessage.ApplicationId = message.ApplicationId;
            responseMessage.Username = message.Username;
            responseMessage.Password = message.Password;

            Bus.Reply(responseMessage);

        }
    }
}
