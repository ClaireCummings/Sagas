﻿using System;
using System.IO;
using System.Windows;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;

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
            ApplicationIdTextBox.Text = Guid.NewGuid().ToString();
        }

        private void SubmitEdrsButton_Click(object sender, RoutedEventArgs e)
        {
            App.Bus.Send(new SubmitEdrs
                {
                    ApplicationId = ApplicationIdTextBox.Text,
                    Username = "BGUser001",
                    Password = "LandReg001",
                    Payload = File.ReadAllText(@"E:\Git\Sagas\LFMSubmissions\LFM.Submissions.Client\TestXML\eDRS Test 4 XmlRequest.xml")
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
                Payload = File.ReadAllText(@"E:\Git\Sagas\LFMSubmissions\LFM.Submissions.Client\TestXML\Attachment Service Test 8 XmlRequest.xml")
            });
        }
    }
}
