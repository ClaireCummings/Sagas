using System.Collections.Generic;
using System.Reflection;
using Autofac;
using LFM.Submissions.AgentComms.LandRegistry;
using LFM.Submissions.Contract.LandRegistry;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using LFM.Submissions.InternalMessages.LandRegistry.Messages;
using Module = Autofac.Module;

namespace LFM.Submissions.AgentComms
{
    public class AgentCommsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var agentServices = Assembly.Load("LFM.Submissions.AgentServices");
            //builder.RegisterAssemblyTypes(agentServices).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(agentServices)
                   .Where(t => t.Name.EndsWith("Sender"))
                   .As(typeof(IEdrsSender));
            builder.RegisterAssemblyTypes(agentServices)
                   .Where(t => t.Name.EndsWith("AttachmentSender"))
                   .As(typeof (IEdrsAttachmentSender));
            builder.RegisterAssemblyTypes(agentServices)
                   .Where(t => t.Name.Equals("CorrespondencePoller"))
                   .As(typeof (IEdrsPoller<CorrespondenceReceived>));
            builder.RegisterAssemblyTypes(agentServices)
                    .Where(t => t.Name.Equals("EarlyCompletionPoller"))
                    .As(typeof(IEdrsPoller<EarlyCompletionReceived>));
            builder.RegisterAssemblyTypes(agentServices)
                    .Where(t => t.Name.Equals("EdrsAttachmentPoller"))
                    .As(typeof(IEdrsPoller<IEdrsAttachmentResponseReceived>));
            builder.RegisterAssemblyTypes(agentServices)
                   .Where(t => t.Name.Equals("OutstandingRequestsPoller"))
                   .As(typeof(IEdrsPoller<List<IOutstandingRequest>>));
            builder.RegisterAssemblyTypes(agentServices)
                   .Where(t => t.Name.Equals("EdrsPoller"))
                   .As(typeof (IEdrsPoller<IEdrsResponseReceived>));
        }
    }
}
