using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LFM.Submissions.InternalMessages.LandRegistry;

namespace LFM.Submissions.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateIdButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateIdButton.Content = Guid.NewGuid().ToString();
        }

        private void SubmitEdrsButton_Click(object sender, RoutedEventArgs e)
        {
            App.Bus.Send(new SubmitEdrs
                {
                    ApplicationId = ApplicationIdTextBox.Text,
                    Username = "BGUser001",
                    Password = "LandReg001",
                    Payload = "<payload xml goes here>"
                });
        }

        private void SubmitEdrsAttachmentButton_Click(object sender, RoutedEventArgs e)
        {
            App.Bus.Send(new SubmitEdrsAttachment
            {
                AttachmentId = Guid.NewGuid().ToString(),
                ApplicationId = ApplicationIdTextBox.Text,
                Username = "BGUser001",
                Password = "LandReg001",
                Payload = "<payload xml goes here>"
            });
        }
    }
}
