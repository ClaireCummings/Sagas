using FakeItEasy;
using LFM.Submissions.AgentComms.LandRegistry;
using LFM.Submissions.AgentServices.LandRegistry;
using LFM.Submissions.Contract.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus;
using NServiceBus.Testing;
using Xunit.Extensions;

namespace LFM.Submissions.LandRegistry.UnitTests
{
    public class When_a_pollcorrespondence_message_is_processed_by_the_agent_comms
    {
        [Theory, LandRegistryTestConventions]
        public void Agent_services_will_publish_an_CorrespondenceReceived_message(PollCorrespondence message)
        {
            var fakeCorrespondencePoller = A.Fake<IEdrsPoller<CorrespondenceReceived>>();
            A.CallTo(() => fakeCorrespondencePoller.Submit()).Returns(true);
            A.CallTo(() => fakeCorrespondencePoller.Response).Returns(new CorrespondenceReceived()
                {
                    Username = message.Username
                });

            MessageConventionExtensions.IsCommandTypeAction = t => t.Namespace != null && t.Namespace.Contains("Commands") && !t.Namespace.StartsWith("NServiceBus");
            
            Test.Initialize();
            Test.Handler<PollCorrespondenceProcessor>()
                .WithExternalDependencies(h => h.CorrespondencePoller = fakeCorrespondencePoller)
                .WithExternalDependencies(h => h.Data = new PollCorrespondenceSagaData())
                .ExpectPublish<CorrespondenceReceived>(m => m.Username == message.Username)
                .OnMessage<PollCorrespondence>(message);
        }
    }
}
