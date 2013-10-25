using System;
using LFM.Submissions.GovGateway.EdrsSubmissionService;
using LFM.Submissions.GovGateway.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;
using NServiceBus;
using Xunit;
using NServiceBus.Testing;
using FakeItEasy;
namespace LFM.Submissions.LandRegistry.UnitTests
{
    public class When_client_submits_an_edrs
    {
        [Fact]
        public void submitedrs_is_forwarded_onto_the_gateway_by_the_backend()
        {
            var submitEdrsMessage = new SubmitEdrs
                {
                    ApplicationId = Guid.NewGuid().ToString(),
                    Username = "username",
                    Password = "password",
                    Payload = "payload"
                };
            MessageConventionExtensions.IsCommandTypeAction = t => t.Namespace != null && t.Namespace.Contains("Commands") && !t.Namespace.StartsWith("NServiceBus"); ;
            Test.Initialize();
            Test.Handler<EdrsProcessor>()
                .WithExternalDependencies(h=>h.Data = new EdrsProcessorSagaData())
                .ExpectSend<SubmitEdrs>(m => m.ApplicationId == submitEdrsMessage.ApplicationId)
                .OnMessage<SubmitEdrs>(submitEdrsMessage);
        }
    }

    public class When_govgateway_receives_submitedrs_with_an_invalid_payload
    {
        [Fact]
        public void gateway_will_reply_with_the_invalid_payload_response()
        {
            var submitEdrsMessage = new SubmitEdrs
                {
                    ApplicationId = Guid.NewGuid().ToString(),
                    Username = "username",
                    Password = "password",
                    Payload = "payload"
                };

            MessageConventionExtensions.IsMessageTypeAction =t => t.Namespace != null && t.Namespace.Contains("Messages") && !t.Namespace.StartsWith("NServiceBus");;
            Test.Initialize();
            Test.Handler<SubmitEdrsProcessor>()
                .WithExternalDependencies(h=>h.EdrsSender = new EdrsSender())
                .ExpectReply<InvalidEdrsPayload>(m => m.ApplicationId == submitEdrsMessage.ApplicationId)
                .OnMessage<SubmitEdrs>(submitEdrsMessage);
        }
    }

    public class When_govgateway_receives_submitedrs_which_is_acknowledged_by_the_Land_Registry
    {
        [Fact]
        public void gateway_will_respond_with_an_acknowledgment_received_message()
        {
            var fakeEdrsSender = A.Fake<IEdrsSender>();
            var fakeEdrsResponseAnalyser = A.Fake<IEdrsResponseAnalyser>();
            
            A.CallTo(() => fakeEdrsSender.Submit())
                .Returns(new ResponseApplicationToChangeRegisterV1_0Type());
            
            A.CallTo(() => fakeEdrsResponseAnalyser
                .GetEdrsResponse(A<ResponseApplicationToChangeRegisterV1_0Type>.Ignored))
                .Returns(new EdrsAcknowledgementReceived());

            var submitEdrsMessage = new SubmitEdrs
            {
                ApplicationId = Guid.NewGuid().ToString(),
                Username = "username",
                Password = "password",
                Payload = "payload"
            };

            MessageConventionExtensions.IsMessageTypeAction = t => t.Namespace != null && t.Namespace.Contains("Messages") && !t.Namespace.StartsWith("NServiceBus"); ;
            Test.Initialize();
            Test.Handler<SubmitEdrsProcessor>()
                .WithExternalDependencies(h => h.EdrsSender = fakeEdrsSender)
                .WithExternalDependencies(h => h.EdrsResponseAnalyser = fakeEdrsResponseAnalyser)
                .ExpectReply<EdrsAcknowledgementReceived>(m => m.ApplicationId == submitEdrsMessage.ApplicationId)
                .OnMessage<SubmitEdrs>(submitEdrsMessage);
        }
    }
}

