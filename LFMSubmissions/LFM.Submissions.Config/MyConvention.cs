using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Config.ConfigurationSource;

namespace LFM.Submissions.Config
{
    public static class MyConvention
    {
        public static Configure MyMessageConventions(this Configure config)
        {
            config.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith(".Commands"));
            config.DefiningMessagesAs(t => t.Namespace != null && t.Namespace.EndsWith(".Messages"));
            config.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains(".Contract."));
            return config;
        }
    }
}
