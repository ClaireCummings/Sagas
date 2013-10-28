using Autofac;
using LFM.Submissions.Config;
using NServiceBus;

namespace LFM.Submissions.AgentComms
{
    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
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