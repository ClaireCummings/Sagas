using System;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public class SubmitEdrsAttachmentProcessor : IHandleMessages<SubmitEdrsAttachment>
    {
        public IBus Bus { get; set; }

        public void Handle(SubmitEdrsAttachment message)
        {
            Console.WriteLine("GovGateway received message SubmitEdrsAttachment AttachmentId: " + message.AttachmentId);

            var sender = new EdrsAttachmentSender
            {
                ApplicationId = message.ApplicationId,
                AttachmentId = message.AttachmentId,
                Username = message.Username,
                Password = message.Password,
                Payload = message.Payload
            };

            var response = sender.Submit();

            var responseMessage = EdrsResponseAnalyser.GetEdrsResponse(response);

            responseMessage.ApplicationId = message.ApplicationId;
            responseMessage.AttachmentId = message.AttachmentId;
            responseMessage.Username = message.Username;
            responseMessage.Password = message.Password;

            Bus.Reply(responseMessage);
        }
    }
}
