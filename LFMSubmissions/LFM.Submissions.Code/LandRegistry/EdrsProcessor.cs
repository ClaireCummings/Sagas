using System;
using LFM.Submissions.InternalMessages.LandRegistry;
using NServiceBus;
using NServiceBus.Saga;

namespace LFM.Submissions.LandRegistry
{
    public class EdrsProcessor : Saga<EdrsProcessorSagaData>, IAmStartedByMessages<SubmitEdrs>, IHandleMessages<SubmitEdrsAttachment>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<SubmitEdrs>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<SubmitEdrsAttachment>(s => s.ApplicationId, m => m.ApplicationId);
        }

        public void Handle(SubmitEdrs message)
        {
            this.Data.ApplicationId = message.ApplicationId;
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1}", message.GetType().ToString(),message.ApplicationId);
        }

        public void Handle(SubmitEdrsAttachment message)
        {
            this.Data.ApplicationId = message.ApplicationId;
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1} AttachmentId: {2}", message.GetType().ToString(), message.ApplicationId, message.AttachmentId);
        }
    }

    public class EdrsProcessorSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public string ApplicationId { get; set; }
    }
}
