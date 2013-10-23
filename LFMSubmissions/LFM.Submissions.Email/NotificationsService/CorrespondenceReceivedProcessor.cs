﻿using System;
using LFM.Submissions.Contract.LandRegistry;
using LFM.Submissions.Contract.NotificationsService;
using NServiceBus;

namespace LFM.Submissions.NotificationsService
{
    public class CorrespondenceReceivedProcessor : IHandleMessages<CorrespondenceReceived>
    {
        public IBus Bus { get; set; }

        public void Handle(CorrespondenceReceived message)
        {
            Console.WriteLine("This will email the correspondence Filename:{0}", message.Filename);
            Bus.Publish(new CorrespondenceSent
                {
                    CorrespondenceId = message.CorrespondenceId,
                    Username = message.Username
                });
        }
    }
}
