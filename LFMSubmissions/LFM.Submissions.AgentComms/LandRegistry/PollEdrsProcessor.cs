using System;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public class PollEdrsProcessor : IHandleMessages<PollEdrs>
    {
        public IBus Bus { get; set; }
        public IEdrsSender EdrsSender { get; set; }

        public void Handle(PollEdrs message)
        {
            Console.WriteLine("Received message " + message.GetType().Name);
            
            EdrsSender.ApplicationId = message.ApplicationId;
            EdrsSender.Username = message.Username;
            EdrsSender.Password = message.Password;

            if (EdrsSender.Poll())
            {
                var responseMessage = EdrsSender.Response;

                responseMessage.ApplicationId = message.ApplicationId;
                responseMessage.Username = message.Username;
                responseMessage.Password = message.Password;

                Bus.Send(responseMessage);
            }
        }
    }
}
