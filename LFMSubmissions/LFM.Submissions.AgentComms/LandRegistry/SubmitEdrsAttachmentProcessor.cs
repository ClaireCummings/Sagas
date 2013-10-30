using System;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public class SubmitEdrsAttachmentProcessor : IHandleMessages<SubmitEdrsAttachment>
    {
        public IBus Bus { get; set; }
        public IEdrsAttachmentSender EdrsAttachmentSender { get; set; }

        public void Handle(SubmitEdrsAttachment message)
        {
            Console.WriteLine("GovGateway received message SubmitEdrsAttachment AttachmentId: " + message.AttachmentId);

            EdrsAttachmentSender.ApplicationMessageId = message.ApplicationId;
            EdrsAttachmentSender.MessageId = message.AttachmentId;
            EdrsAttachmentSender.Username = message.Username;
            EdrsAttachmentSender.Password = message.Password;
            EdrsAttachmentSender.Payload = message.Payload;

            if (EdrsAttachmentSender.Submit())
            {
                var responseMessage = EdrsAttachmentSender.Response;

                responseMessage.ApplicationId = message.ApplicationId;
                responseMessage.AttachmentId = message.AttachmentId;
                responseMessage.Username = message.Username;
                responseMessage.Password = message.Password;

                Bus.Reply(responseMessage);
            }
        }
    }
}
