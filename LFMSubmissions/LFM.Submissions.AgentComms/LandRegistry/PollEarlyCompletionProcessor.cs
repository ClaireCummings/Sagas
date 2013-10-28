using System;
using LFM.Submissions.AgentServices.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    public class PollEarlyCompletionProcessor : IHandleMessages<PollEarlyCompletion>
    {
        public IBus Bus { get; set; }
        public void Handle(PollEarlyCompletion message)
        {
            Console.WriteLine("Gateway received message PollEarlyCompletion");

            var sender = new EarlyCompletionPollSender
            {
                MessageId = message.MessageId,
                Username = message.Username,
                Password = message.Password,
            };

            var response = sender.Poll();

            var responseMessage = EarlyCompletionPollResponseAnalyser.GetEarlyCompletionResponse(response);

            if (responseMessage != null)
            {
                responseMessage.ApplicationMessageId = message.MessageId;
                responseMessage.Username = message.Username;
                Bus.Publish(responseMessage);
            }
        }
    }
}
