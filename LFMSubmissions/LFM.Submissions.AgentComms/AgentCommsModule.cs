using Autofac;
using LFM.Submissions.AgentServices.LandRegistry;

namespace LFM.Submissions.AgentComms
{
    public class AgentCommsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EdrsSender>().As<IEdrsSender>();
            builder.RegisterType<EdrsResponseAnalyser>().As<IEdrsResponseAnalyser>();
        }
    }
}
