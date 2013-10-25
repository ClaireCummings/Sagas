using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using LFM.Submissions.GovGateway.LandRegistry;

namespace LFM.Submissions.GovGateway
{
    public class GovGatewayModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EdrsSender>().As<IEdrsSender>();
            builder.RegisterType<EdrsResponseAnalyser>().As<IEdrsResponseAnalyser>();
        }
    }
}
