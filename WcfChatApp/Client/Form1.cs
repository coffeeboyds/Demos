using ChatLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private Guid _userId;
        private string _userName;
        
        private ServerChatProxy _serverChatProxy;
        private IServerChatService _serverChannel;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                if (_serverChannel != null)
                    _serverChannel.Logoff(_userId);

                if (_serverChatProxy != null)
                    _serverChatProxy.Close();
            }
            catch(Exception)
            {

            }

            base.OnClosing(e);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTextbox.Text))
            {
                MessageBox.Show("Username can not be null/whitespace");
                return;
            }

            usernameTextbox.Enabled = false;
            loginButton.Enabled = false;

            _userName = usernameTextbox.Text;
            _userId = Guid.NewGuid();

            var clientChatService = new ClientChatService();
            clientChatService.ConnectedClientsListUpdatedEvent += UpdateListOfConnectedClients;
            clientChatService.NewMessageReceivedEvent += DisplayNewMessage;

            // Send clientChatService to the server as our callback, so the server can signal us when needed.
            _serverChatProxy = new ServerChatProxy(clientChatService);
            _serverChannel = _serverChatProxy.ChannelFactory.CreateChannel();

            try
            {
                _serverChannel.Logon(_userId, _userName);
            }
            catch(Exception)
            {
                MessageBox.Show("Failed to logon. Make sure the server application is running.");
                usernameTextbox.Enabled = true;
                loginButton.Enabled = true;
                return;
            }

            sendButton.Enabled = true;
            messageTextbox.Enabled = true;
        }

        private void UpdateListOfConnectedClients(List<ConnectedClient> connectedClients)
        {
            clientsListbox.DataSource = connectedClients;
            clientsListbox.DisplayMember = "Username";
            clientsListbox.ValueMember = "Id";
        }

        private void DisplayNewMessage(string message)
        {
            messagesListbox.Items.Add(message);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(messageTextbox.Text))
            {
                MessageBox.Show("Please enter a message first.");
                return;
            }

            var selectedClientRecipient = clientsListbox.SelectedItem as ConnectedClient;

            if (selectedClientRecipient == null)
            {
                MessageBox.Show("Please select a recipient first.");
                return;
            }

            _serverChannel.SendMessage(_userId, selectedClientRecipient.Id, messageTextbox.Text);
        }
    }
}
