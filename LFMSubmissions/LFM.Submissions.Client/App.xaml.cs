using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LFM.Submissions.Config;
using NServiceBus;
using NServiceBus.Installation.Environments;

namespace LFM.Submissions.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IBus Bus { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Configure.Serialization.Xml();
            Bus = Configure.With()
                .DefaultBuilder()
                .MyMessageConventions()
                .UseTransport<Msmq>()
                .UnicastBus()
                .LoadMessageHandlers()
                .CreateBus()
                .Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
        }
    }
}
