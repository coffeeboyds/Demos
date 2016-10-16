using ChatLibrary;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        private ServiceHost _host;
        
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            _host.Close();
            base.OnClosed(e);
        }

        private void UpdateListOfClients(List<string> names)
        {
            clientsListbox.Items.Clear();

            foreach (string client in names)
                clientsListbox.Items.Add(client);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create a URI to serve as the base address
            var httpUrl = new Uri("net.tcp://localhost:8090/ChatLibrary/ServerChatService");

            // Create ServiceHost
            _host = new ServiceHost(typeof(ServerChatService), httpUrl);

            // Add a service endpoint
            _host.AddServiceEndpoint(typeof(IServerChatService), new NetTcpBinding(), "");

            // Enable metadata exchange
            var smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = false;
            _host.Description.Behaviors.Add(smb);

            _host.Open();

            ServerChatService.UpdateListOfConnectedClients += new ServerChatService.RefreshListOfNames(UpdateListOfClients);
        }
    }
}
