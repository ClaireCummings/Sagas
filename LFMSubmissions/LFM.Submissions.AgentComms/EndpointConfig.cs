using Autofac;
using LFM.Submissions.Config;
using NServiceBus;

namespace LFM.Submissions.AgentComms
{
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
	    public void Init()
	    {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AgentCommsModule>();
            
            Configure.With()
                .MyMessageConventions()
                .AutofacBuilder(builder.Build());
	    }
    }
}