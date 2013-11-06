using System;
using System.Collections.Generic;
using System.IO;
using LFM.ApplicationServices;
using LFM.ApplicationServices.Submissions;
using LFM.Submissions.Contract.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;
using NServiceBus;
using NServiceBus.Saga;

namespace LFM.Submissions.LandRegistry
{
    public class EdrsProcessor : Saga<EdrsProcessorSagaData>, IAmStartedByMessages<SubmitEdrs>, IAmStartedByMessages<SubmitEdrsAttachment>, 
        IHandleMessages<PollEdrs>, IHandleMessages<PollEdrsAttachment>,
        IHandleMessages<EdrsAcknowledgementReceived>, IHandleMessages<EdrsRejectionReceived>,IHandleMessages<EdrsResultsReceived>, IHandleMessages<EdrsOtherReceived>,
        IHandleMessages<EdrsAttachmentAcknowledgementReceived>, IHandleMessages<EdrsAttachmentRejectionReceived>, IHandleMessages<EdrsAttachmentResultsReceived>, IHandleMessages<EdrsAttachmentOtherReceived>,
        IHandleMessages<EarlyCompletionReceived>, IHandleMessages<CorrespondenceReceived>, IHandleMessages<InvalidEdrsPayload>

    {
        public ISubmissionAdministrationService AdministrationService { get; set; }
        public ICommandInvoker CommandInvoker { get; set; }

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<SubmitEdrs>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<SubmitEdrsAttachment>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<EdrsAcknowledgementReceived>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<EdrsRejectionReceived>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<EdrsResultsReceived>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<EdrsOtherReceived>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<PollEdrs>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<PollEdrsAttachment>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<EdrsAttachmentAcknowledgementReceived>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<EdrsAttachmentRejectionReceived>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<EdrsAttachmentResultsReceived>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<EdrsAttachmentOtherReceived>(s => s.ApplicationId, m => m.ApplicationId);
            ConfigureMapping<EarlyCompletionReceived>(s => s.ApplicationId, m => m.ApplicationMessageId);
            ConfigureMapping<InvalidEdrsPayload>(s => s.ApplicationId, m => m.ApplicationId);
        }

        public void Handle(SubmitEdrs message)
        {
            this.Data.ApplicationId = message.ApplicationId;

            CommandInvoker.Execute<CreateSubmissionCommand, CreateSubmissionQueryResult>(new CreateSubmissionCommand()
                {
                    AgentUsername = message.Username,
                    ApplicationId = message.ApplicationId,
                    Payload = message.Payload,
                    // Assume automatic authorisation
                    Status = SubmissionStatus.AuthorisedAccepted
                });
                        
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1}", message.GetType().Name,message.ApplicationId);
            Bus.Send(message);
        }

        public void Handle(SubmitEdrsAttachment message)
        {
            this.Data.ApplicationId = message.ApplicationId;
            if (Data.Attachments == null)
                Data.Attachments = new Dictionary<string, EdrsResponse>();
            Data.Attachments.Add(message.AttachmentId,EdrsResponse.None);
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1} AttachmentId: {2}", message.GetType().Name, message.ApplicationId, message.AttachmentId);
            Bus.Send(message);
        }

        public void Handle(PollEdrs message)
        {
            if (this.Data.EdrsResponse == EdrsResponse.Acknowledgement)
            {
               Bus.Send(message);
            }
        }

        public void Handle(EdrsAcknowledgementReceived message)
        {
            //todo persist the response to the backend database
            this.Data.EdrsResponse = EdrsResponse.Acknowledgement;
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1}", message.GetType().Name, message.ApplicationId);

            // TODO:  Defer poll according to response expected datetime
            Bus.Defer(TimeSpan.FromSeconds(5),
                new PollEdrs
                {
                    ApplicationId = message.ApplicationId,
                    Username = message.Username,
                    Password = message.Password
                });

            // Ensure outstanding requests are polled for for this user
            Bus.Send(new PollForOutstandingRequests {ApplicationId = message.ApplicationId, Username = message.Username, Password = message.Password});
        }

        public void Handle(EdrsRejectionReceived message)
        {
            this.Data.EdrsResponse = EdrsResponse.Rejection;
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1}", message.GetType().Name, message.ApplicationId);
            EndProcessing(message.ApplicationId,message.Username);
        }

        

        public void Handle(EdrsResultsReceived message)
        {
            this.Data.EdrsResponse = EdrsResponse.Results;
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1}", message.GetType().Name, message.ApplicationId);
            EndProcessing(message.ApplicationId, message.Username);
        }

        public void Handle(EdrsOtherReceived message)
        {
            this.Data.EdrsResponse = EdrsResponse.Other;
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1}", message.GetType().Name, message.ApplicationId);
            EndProcessing(message.ApplicationId, message.Username);
        }

        public void Handle(EdrsAttachmentAcknowledgementReceived message)
        {
            //todo persist the response to the backend database
            this.Data.Attachments[message.AttachmentId] = EdrsResponse.Acknowledgement;

            Console.WriteLine("Land Registry Received {0} ApplicationId: {1} AttachmentId: {2}", message.GetType().Name, message.ApplicationId, message.AttachmentId);

            // TODO: Use expected response datetime in defer
            Bus.Defer(TimeSpan.FromSeconds(5),
                new PollEdrsAttachment
                {
                    ApplicationId = message.ApplicationId,
                    AttachmentId = message.AttachmentId,
                    Username = message.Username,
                    Password = message.Password
                });
        }

        public void Handle(EdrsAttachmentRejectionReceived message)
        {
            this.Data.Attachments[message.AttachmentId] = EdrsResponse.Rejection;
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1} AttachmentId: {2}", message.GetType().Name, message.ApplicationId, message.AttachmentId);
        }

        public void Handle(EdrsAttachmentResultsReceived message)
        {
            this.Data.Attachments[message.AttachmentId] = EdrsResponse.Results;
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1} AttachmentId: {2}", message.GetType().Name, message.ApplicationId, message.AttachmentId);
        }

        public void Handle(EdrsAttachmentOtherReceived message)
        {
            this.Data.Attachments[message.AttachmentId] = EdrsResponse.Other;
            Console.WriteLine("Land Registry Received {0} ApplicationId: {1} AttachmentId: {2}", message.GetType().Name, message.ApplicationId, message.AttachmentId);
        }

        public void Handle(PollEdrsAttachment message)
        {
            Console.WriteLine("Received PolEdrsAttachement message for AttachmentId" + message.AttachmentId);
            if (this.Data.EdrsResponse == EdrsResponse.None || this.Data.EdrsResponse == EdrsResponse.Acknowledgement)
            {
                if (Data.Attachments !=null && Data.Attachments.ContainsKey(message.AttachmentId) && Data.Attachments[message.AttachmentId] == EdrsResponse.Acknowledgement)
                    Bus.Send(message);
            }
        }
        
        public void EndProcessing(string applicationId, string username)
        {
            MarkAsComplete();
            Bus.Send(new StopPollingForOutstandingRequests
                {
                    ApplicationId = applicationId,
                    Username = username
                });
        }

        public void Handle(EarlyCompletionReceived message)
        {
            Console.WriteLine("EarlyCompletion for ApplicationID: " + message.ApplicationMessageId);
            // Early completion does not end the Edrs Saga -- still need to await the actual Results
        }

        public void Handle(InvalidEdrsPayload message)
        {
            Console.WriteLine("Application {0} has an invalid payload", message.ApplicationId);
            MarkAsComplete();
        }

        public void Handle(CorrespondenceReceived message)
        {
            Console.WriteLine("CorrespondanceReceived for ApplicationId: " + message.ApplicationMessageId);
        }
    }

    public class EdrsProcessorSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public string ApplicationId { get; set; }
        public EdrsResponse EdrsResponse { get; set; }

        public Dictionary<string, EdrsResponse> Attachments { get; set; }
    }
}
