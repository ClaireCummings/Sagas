using System;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;
using NServiceBus;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public class PollEdrsAttachmentProcessor : IHandleMessages<PollEdrsAttachment>
    {
        public IBus Bus { get; set; }
        public IEdrsPoller<IEdrsAttachmentResponseReceived> EdrsAttachmentPoller { get; set; }

        public void Handle(PollEdrsAttachment message)
        {
            Console.WriteLine("Gateway received message PollEdrsAttachment MessageId: " + message.AttachmentId);

            EdrsAttachmentPoller.MessageId = message.AttachmentId;
            EdrsAttachmentPoller.Username = message.Username;
            EdrsAttachmentPoller.Password = message.Password;

            if (EdrsAttachmentPoller.Submit())
            {
                var responseMessage = EdrsAttachmentPoller.Response;

                responseMessage.ApplicationId = message.ApplicationId;
                responseMessage.AttachmentId = message.AttachmentId;
                responseMessage.Username = message.Username;
                responseMessage.Password = message.Password;

                Bus.Send(responseMessage);
            }
        }
    }
}
