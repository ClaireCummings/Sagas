
using Autofac;
using Autofac.Features.ResolveAnything;
using LFM.Submissions.Config;

namespace LFM.Submissions.GovGateway
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
	    public void Init()
	    {
            var builder = new ContainerBuilder();
          // builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterModule<GovGatewayModule>();

            Configure.With()
                .MyMessageConventions()
                .AutofacBuilder(builder.Build());
	    }
    }
}