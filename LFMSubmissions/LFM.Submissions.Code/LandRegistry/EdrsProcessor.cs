using System;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;
using NServiceBus;
using NServiceBus.Saga;

namespace LFM.Submissions.LandRegistry
{
    public class EdrsProcessor : Saga<EdrsProcessorSagaData>, IAmStartedByMessages<SubmitEdrs>, IAmStartedByMessages<SubmitEdrsAttachment>, 
            IHandleMessages<EdrsAcknowledgementReceived>, IHandleMessages<PollEdrs>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<SubmitEdrs>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<SubmitEdrsAttachment>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<EdrsAcknowledgementReceived>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<PollEdrs>(s => s.ApplicationId, m => m.ApplicationId);
        }

        public void Handle(SubmitEdrs message)
        {
            this.Data.ApplicationId = message.ApplicationId;
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1}", message.GetType().Name,message.ApplicationId);
            Bus.Send(message);
        }

        public void Handle(SubmitEdrsAttachment message)
        {
            this.Data.ApplicationId = message.ApplicationId;
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1} AttachmentId: {2}", message.GetType().Name, message.ApplicationId, message.AttachmentId);
        }

        public void Handle(EdrsAcknowledgementReceived message)
        {
            //todo persist the response to the backend database
            this.Data.EdrsResponse = EdrsResponse.Acknowledgement;
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1}", message.GetType().Name, message.ApplicationId);

            Bus.Defer(TimeSpan.FromSeconds(5),
                new PollEdrs
                {
                    ApplicationId = message.ApplicationId,
                    Username = message.Username,
                    Password = message.Password
                });

        }

        public void Handle(PollEdrs message)
        {

            Bus.Send(message);
        }
    }

    public class EdrsProcessorSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public string ApplicationId { get; set; }
        public EdrsResponse EdrsResponse { get; set; }
    }
}
