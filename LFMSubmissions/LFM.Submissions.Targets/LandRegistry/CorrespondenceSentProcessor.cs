using System;
using LFM.Submissions.Contract.NotificationsService;
using NServiceBus;

namespace LFM.Submissions.GovGateway.LandRegistry
{
    public class CorrespondenceSentProcessor : IHandleMessages<CorrespondenceSent>
    {
        public void Handle(CorrespondenceSent message)
        {
            Console.WriteLine("Correspondence {0} Sent", message.CorrespondenceId);
        }
    }
}
