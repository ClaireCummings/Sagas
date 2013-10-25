using System;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;
using NServiceBus;

namespace LFM.Submissions.GovGateway.LandRegistry
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
                Bus.Reply(responseMessage);
            }
            catch (Exception ex)
            {
                Bus.Reply(new InvalidEdrsPayload
                    {
                        ApplicationId = message.ApplicationId
                    });
            }
        }
    }
}
