using System;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public class PollEarlyCompletionProcessor : IHandleMessages<PollEarlyCompletion>
    {
        public IBus Bus { get; set; }
        public IEdrsEarlyCompletionPoller EdrsEarlyCompletionPoller { get; set; }

        public void Handle(PollEarlyCompletion message)
        {
            Console.WriteLine("Gateway received message PollEarlyCompletion");

            EdrsEarlyCompletionPoller.MessageId = message.MessageId;
            EdrsEarlyCompletionPoller.Username = message.Username;
            EdrsEarlyCompletionPoller.Password = message.Password;

            if (EdrsEarlyCompletionPoller.Poll())
            {
                var responseMessage = EdrsEarlyCompletionPoller.Response;

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
