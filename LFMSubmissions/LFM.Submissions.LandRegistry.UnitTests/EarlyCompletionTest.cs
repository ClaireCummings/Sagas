using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class When_a_early_completion_message_is_processed_by_the_agent_comms
    {
        [Theory, LandRegistryTestConventions]
        public void Agent_services_will_publish_an_EarlyCompletionReceived_message(PollEarlyCompletion message)
        {
            var fakeEarlyCompletionPoller = A.Fake<IEdrsPoller<EarlyCompletionReceived>>();
            A.CallTo(() => fakeEarlyCompletionPoller.Poll()).Returns(true);
            A.CallTo(() => fakeEarlyCompletionPoller.Response).Returns(new EarlyCompletionReceived()
                {
                    Username = message.Username
                });

            MessageConventionExtensions.IsCommandTypeAction =t => t.Namespace != null && t.Namespace.Contains("Commands") && !t.Namespace.StartsWith("NServiceBus");
            Test.Initialize();
            Test.Handler<PollEarlyCompletionProcessor>()
                .WithExternalDependencies(h => h.EdrsPoller = fakeEarlyCompletionPoller)
                .ExpectPublish<EarlyCompletionReceived>(m => m.Username == message.Username)
                .OnMessage<PollEarlyCompletion>(message);
        }
    }
}
