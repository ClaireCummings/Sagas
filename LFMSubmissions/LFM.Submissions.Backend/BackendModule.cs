using System.Data.Entity;
using Autofac;
using LFM.Infrastructure.Submissions;

namespace LFM.Submissions.Backend
{
    public class BackendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof (SubmissionRepository).Assembly)
                   .AsImplementedInterfaces();
            builder.RegisterType<SubmissionsContext>()
                   .AsSelf()
                   .InstancePerLifetimeScope();
        }
    }
}