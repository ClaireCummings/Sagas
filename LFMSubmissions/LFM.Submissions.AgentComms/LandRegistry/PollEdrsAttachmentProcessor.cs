using System;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public class PollEdrsAttachmentProcessor : IHandleMessages<PollEdrsAttachment>
    {
        public IBus Bus { get; set; }
        public IEdrsAttachmentSender EdrsAttachmentSender { get; set; }

        public void Handle(PollEdrsAttachment message)
        {
            Console.WriteLine("Gateway received message PollEdrsAttachment AttachmentId: " + message.AttachmentId);

            EdrsAttachmentSender.ApplicationId = message.ApplicationId;
            EdrsAttachmentSender.AttachmentId = message.AttachmentId;
            EdrsAttachmentSender.Username = message.Username;
            EdrsAttachmentSender.Password = message.Password;

            if (EdrsAttachmentSender.Poll())
            {
                var responseMessage = EdrsAttachmentSender.Response;

                responseMessage.ApplicationId = message.ApplicationId;
                responseMessage.AttachmentId = message.AttachmentId;
                responseMessage.Username = message.Username;
                responseMessage.Password = message.Password;

                Bus.Send(responseMessage);
            }
        }
    }
}
