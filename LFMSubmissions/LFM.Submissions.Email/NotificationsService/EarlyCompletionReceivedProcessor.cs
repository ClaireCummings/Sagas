using System;
using LFM.Submissions.Contract.LandRegistry;
using NServiceBus;

namespace LFM.Submissions.NotificationsService
{
    public class EarlyCompletionReceivedProcessor : IHandleMessages<EarlyCompletionReceived>
    {
        public void Handle(EarlyCompletionReceived message)
        {
            Console.WriteLine("Notification Service received EarlyCompletionReceived");
            // TOdO: eMAIL customer
        }
    }
}
