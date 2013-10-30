using System.Reflection;
using Autofac;
using LFM.Submissions.AgentComms.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;
using Module = Autofac.Module;

namespace LFM.Submissions.AgentComms
{
    public class AgentCommsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var agentServices = Assembly.Load("LFM.Submissions.AgentServices");

            builder.RegisterAssemblyTypes(agentServices).AsImplementedInterfaces();
        }
    }
}
