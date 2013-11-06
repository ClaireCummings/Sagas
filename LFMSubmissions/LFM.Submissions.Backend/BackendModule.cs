using Autofac;
using LFM.Infrastructure.Submissions;

namespace LFM.Submissions.Backend
{
    public class BackendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof (SubmissionAdministrationService).Assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces();
//            builder.RegisterAssemblyTypes(typeof (SubmissionRepository).Assembly)
//                   .AsImplementedInterfaces().WithParameter("context", new SubmissionsContext());
            builder.RegisterAssemblyTypes(typeof (SubmissionRepository).Assembly)
                   .AsImplementedInterfaces();
            builder.RegisterType<SubmissionsContext>()
                   .AsSelf()
                   .InstancePerLifetimeScope();
        }
    }
}