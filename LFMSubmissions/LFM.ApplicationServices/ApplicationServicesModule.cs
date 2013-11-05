using Autofac;
using LFM.ApplicationServices.Submissions;

namespace LFM.ApplicationServices
{
    public class ApplicationServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SubmissionService>().AsImplementedInterfaces();
            builder.RegisterType<CommandInvoker>().As<ICommandInvoker>();
            builder.RegisterType<QueryInvoker>().As<IQueryInvoker>();
        }
    }
}
