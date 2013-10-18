using System;
using LFM.Submissions.InternalMessages.LandRegistry;
using LFM.Submissions.Targets.LandRegistry;
using NServiceBus;
using NServiceBus.Saga;

namespace LFM.Submissions.LandRegistry
{
    public class EdrsProcessor : Saga<EdrsProcessorSagaData>, IAmStartedByMessages<SubmitEdrs>, IAmStartedByMessages<SubmitEdrsAttachment>, IHandleMessages<ProcessEdrsResponse>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<SubmitEdrs>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<SubmitEdrsAttachment>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<ProcessEdrsResponse>(s => s.ApplicationId, m => m.ApplicationId);
        }

        public void Handle(SubmitEdrs message)
        {
            this.Data.ApplicationId = message.ApplicationId;
            Console.WriteLine("Land Registry Received {0}{2}ApplicationId: {1}", message.GetType().ToString(),message.ApplicationId, Environment.NewLine);
            var sender = new EdrsSender
                {
                    ApplicationId = message.ApplicationId,
                    Username = message.Username,
                    Password = message.Password,
                    Payload = message.Payload
                };
            
            var response = sender.Submit();

            Bus.Send(new ProcessEdrsResponse
                {
                    ApplicationId = message.ApplicationId,
                    ResponsePayload = response
                });
        }

        public void Handle(SubmitEdrsAttachment message)
        {
            this.Data.ApplicationId = message.ApplicationId;
            Console.WriteLine("Land Registry Received {0}{3}ApplicationId: {1}{3}AttachmentId: {2}", message.GetType().ToString(), message.ApplicationId, message.AttachmentId, Environment.NewLine);
        }

        public void Handle(ProcessEdrsResponse message)
        {
            //todo persist the response to the backend database
            this.Data.EdrsResponse = new EdrsResponseAnalyser(message.ResponsePayload).EdrsResponse;
            Console.WriteLine("Land Registry Received {0}{3}ApplicationId: {1}{3}Response: {2}", message.GetType().ToString(), message.ApplicationId, this.Data.EdrsResponse.ToString(), Environment.NewLine);
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
