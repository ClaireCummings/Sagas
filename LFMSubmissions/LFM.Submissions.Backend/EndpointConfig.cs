using Autofac;
using LFM.Submissions.Config;

namespace LFM.Submissions.Backend
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
	    public void Init()
	    {
            var builder = new ContainerBuilder();
            builder.RegisterModule<BackendModule>();

            Configure.With()
	                 .MyMessageConventions()
                     .AutofacBuilder(builder.Build());
	    }
    }
}