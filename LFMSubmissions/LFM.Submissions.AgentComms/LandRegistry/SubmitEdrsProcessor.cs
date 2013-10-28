using System;
using LFM.Submissions.AgentServices.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;
using NServiceBus;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public class SubmitEdrsProcessor : IHandleMessages<SubmitEdrs>
    {
        public IBus Bus { get; set; }
        public IEdrsSender EdrsSender { get; set; }
        public IEdrsResponseAnalyser EdrsResponseAnalyser { get; set; }

        public void Handle(SubmitEdrs message)
        {
            EdrsSender.ApplicationId = message.ApplicationId;
            EdrsSender.Username = message.Username;
            EdrsSender.Password = message.Password;
            EdrsSender.Payload = message.Payload;

            try
            {
                var response = EdrsSender.Submit();

                Console.WriteLine("GovGateway EdrsService Responded");
                var responseMessage = EdrsResponseAnalyser.GetEdrsResponse(response);

                responseMessage.ApplicationId = message.ApplicationId;
                responseMessage.Username = message.Username;
                responseMessage.Password = message.Password;
                Bus.Reply((object) responseMessage);
            }
            catch (Exception ex)
            {
                Bus.Reply((object) new InvalidEdrsPayload
                    {
                        ApplicationId = message.ApplicationId
                    });
            }
        }
    }
}
