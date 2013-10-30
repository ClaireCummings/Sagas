using System;
using LFM.Submissions.Contract.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public class PollEarlyCompletionProcessor : IHandleMessages<PollEarlyCompletion>
    {
        public IBus Bus { get; set; }
        public IEdrsPoller<EarlyCompletionReceived> EdrsPoller { get; set; }

        public void Handle(PollEarlyCompletion message)
        {
            Console.WriteLine("Gateway received message PollEarlyCompletion");

            EdrsPoller.MessageId = message.MessageId;
            EdrsPoller.Username = message.Username;
            EdrsPoller.Password = message.Password;

            if (EdrsPoller.Poll())
            {
                var responseMessage = EdrsPoller.Response;

                if (responseMessage != null)
                {
                    responseMessage.ApplicationMessageId = message.MessageId;
                    responseMessage.Username = message.Username;
                    Bus.Publish(responseMessage);
                }
            }
        }
    }
}
