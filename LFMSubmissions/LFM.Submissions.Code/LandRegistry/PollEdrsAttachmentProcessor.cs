using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public class PollEdrsAttachmentProcessor : IHandleMessages<PollEdrsAttachment>
    {
        public IBus Bus { get; set; }

        public void Handle(PollEdrsAttachment message)
        {
            Console.WriteLine("Gateway received message PollEdrsAttachment AttachmentId: " + message.AttachmentId);

            var sender = new EdrsAttachmentSender
            {
                ApplicationId = message.ApplicationId,
                AttachmentId = message.AttachmentId,
                Username = message.Username,
                Password = message.Password,
            };

            
            var response = sender.Poll();

            var responseMessage = EdrsResponseAnalyser.GetEdrsResponse(response);

            responseMessage.ApplicationId = message.ApplicationId;
            responseMessage.AttachmentId = message.AttachmentId;
            responseMessage.Username = message.Username;
            responseMessage.Password = message.Password;

            Bus.Send(responseMessage);
        }
    }
}
