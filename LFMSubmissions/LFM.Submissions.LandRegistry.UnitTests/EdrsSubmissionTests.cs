using LFM.ApplicationServices;
using LFM.ApplicationServices.Submissions;
using LFM.Submissions.AgentComms.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;
using NServiceBus;
using NServiceBus.Testing;
using FakeItEasy;
using Xunit;
using Xunit.Extensions;

namespace LFM.Submissions.LandRegistry.UnitTests
{
    public class When_client_submits_an_edrs
    {
        public ICommandInvoker _fakeCommandInvoker;

        public When_client_submits_an_edrs()
        {
            _fakeCommandInvoker = A.Fake<ICommandInvoker>();
        }

        [Theory, LandRegistryTestConventions]
        public void submitedrs_is_forwarded_onto_the_gateway_by_the_backend(SubmitEdrs submitEdrsMessage)
        {
            MessageConventionExtensions.IsCommandTypeAction = t => t.Namespace != null && t.Namespace.Contains("Commands") && !t.Namespace.StartsWith("NServiceBus");
            Test.Initialize();
            Test.Handler<EdrsProcessor>()
                .WithExternalDependencies(h=>h.Data = new EdrsProcessorSagaData())
                .WithExternalDependencies(h=>h.CommandInvoker = _fakeCommandInvoker)
                .ExpectSend<SubmitEdrs>(m => m == submitEdrsMessage)
                .OnMessage<SubmitEdrs>(submitEdrsMessage);
        }

        [Theory, LandRegistryTestConventions]
        public void a_submission_record_is_saved_to_the_database(SubmitEdrs submitEdrsMessage)
        {
            var sut = A.Fake<EdrsProcessor>();
            var bus = A.Fake<IBus>();

            sut.CommandInvoker = _fakeCommandInvoker;
            sut.Bus = bus;
            sut.Data = new EdrsProcessorSagaData();
            sut.Handle(submitEdrsMessage);

            A.CallTo(() => _fakeCommandInvoker.Execute<CreateSubmissionCommand, CreateSubmissionQueryResult>
                  (A<CreateSubmissionCommand>.That.Matches(c => c.AgentUsername == submitEdrsMessage.Username 
                      && c.ApplicationId == submitEdrsMessage.ApplicationId 
                      && c.Payload == submitEdrsMessage.Payload 
                      && c.Status == SubmissionStatus.AuthorisedAccepted)))
                .MustHaveHappened();
        }
    }

    public class When_agentcomms_receives_submitedrs_with_an_invalid_payload
    {
        [Theory, LandRegistryTestConventions]
        public void gateway_will_reply_with_the_invalid_payload_response_for_that_application(SubmitEdrs submitEdrsMessage)
        {
            var fakeEdrsSender = A.Fake<IEdrsSender>();
            A.CallTo(() => fakeEdrsSender.Submit()).Throws(new InvalidPayloadException()).Once();

            MessageConventionExtensions.IsMessageTypeAction =t => t.Namespace != null && t.Namespace.Contains("Messages") && !t.Namespace.StartsWith("NServiceBus");
            Test.Initialize();
            Test.Handler<SubmitEdrsProcessor>()
                .WithExternalDependencies(h => h.EdrsSender = fakeEdrsSender)
                .ExpectReply<InvalidEdrsPayload>(m => m.ApplicationId == submitEdrsMessage.ApplicationId)
                .OnMessage<SubmitEdrs>(submitEdrsMessage);
        }
    }

    public class When_agentcomms_receives_submitedrs_which_is_acknowledged_by_the_Land_Registry
    {

        [Theory, LandRegistryTestConventions]
        public void gateway_will_respond_with_an_acknowledgment_received_message_for_that_application(SubmitEdrs submitEdrsMessage)
        {
            var fakeEdrsSender = A.Fake<IEdrsSender>();
            
            A.CallTo(() => fakeEdrsSender.Submit())
                .Returns(true);

            A.CallTo(() => fakeEdrsSender
                .Response)
                .Returns(new EdrsAcknowledgementReceived());

            MessageConventionExtensions.IsMessageTypeAction = t => t.Namespace != null && t.Namespace.Contains("Messages") && !t.Namespace.StartsWith("NServiceBus");
            Test.Initialize();
            Test.Handler<SubmitEdrsProcessor>()
                .WithExternalDependencies(h => h.EdrsSender = fakeEdrsSender)
                .ExpectReply<EdrsAcknowledgementReceived>(m => (m.ApplicationId == submitEdrsMessage.ApplicationId && m.Username == submitEdrsMessage.Username))
                .OnMessage<SubmitEdrs>(submitEdrsMessage);
        }
    }
}

