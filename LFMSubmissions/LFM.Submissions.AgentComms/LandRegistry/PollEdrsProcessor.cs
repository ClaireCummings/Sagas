using System;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;
using NServiceBus;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public class PollEdrsProcessor : IHandleMessages<PollEdrs>
    {
        public IBus Bus { get; set; }
        public IEdrsPoller<IEdrsResponseReceived> EdrsPoller { get; set; }
        
        public void Handle(PollEdrs message)
        {
            Console.WriteLine("Received message " + message.GetType().Name);
            
            EdrsPoller.MessageId = message.ApplicationId;
            EdrsPoller.Username = message.Username;
            EdrsPoller.Password = message.Password;

            if (EdrsPoller.Submit())
            {
                var responseMessage = EdrsPoller.Response;

                responseMessage.ApplicationId = message.ApplicationId;
                responseMessage.Username = message.Username;
                responseMessage.Password = message.Password;

                Bus.Send(responseMessage);
            }
        }
    }
}
