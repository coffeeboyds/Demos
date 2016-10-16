using System;
using System.Windows.Forms;
using System.ComponentModel;
using Common.Services;
using MultiServiceMessengerApp.Services;
using System.Collections.Generic;
using MultiServiceMessengerApp.Contracts;
using System.Diagnostics;
using System.IO;
using Common.Models;
using System.ServiceModel;

namespace MultiServiceMessengerApp
{
    public partial class Form1 : Form
    {
        private IAsyncMessengerService _wcfMessengerService;
        private IAsyncMessengerService _webApi2MessengerService;
        private IAsyncMessengerService _selectedMessengerService;

        private Stopwatch _stopwatch;

        protected override void OnClosing(CancelEventArgs e)
        {
            MessageRepositoryService.Instance.Dispose();
            base.OnClosing(e);
        }

        public Form1()
        {
            _stopwatch = new Stopwatch();
            _webApi2MessengerService = new WebApi2MessengerService();
            _wcfMessengerService = new Services.WcfMessengerService();
            _selectedMessengerService = _webApi2MessengerService;

            InitializeComponent();
            webApi2RadioButton.Checked = true;
        }

        private bool IsRadioButtonChecked(object radioButton)
        {
            RadioButton rb = radioButton as RadioButton;

            return rb != null && rb.Checked;
        }

        private void DisplayMessages(IEnumerable<Common.Models.Message> messages)
        {
            MessagesListBox.Items.Clear();

            foreach(var message in messages)
                MessagesListBox.Items.Add(message.Text);
        }

        private async void createMessageButton_Click(object sender, EventArgs e)
        {
            var message = new Common.Models.Message
            {
                Id = MessagesListBox.Items.Count,
                Text = pushMessageTextBox.Text
            };

            _stopwatch.Reset();
            _stopwatch.Start();

            ServerResponse serverResponse = null;

            // This is how error handling is done with WCF.
            // The server can throw an exception back at us.
            // I don't like this way, which is why I'm only doing it here just to show.
            try
            {
                serverResponse = await _selectedMessengerService.CreateMessage(message);
            }
            catch(FaultException invalidMessageFault)
            {
                MessageBox.Show(invalidMessageFault.Message, "FaultException thrown from the server.");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }

            _stopwatch.Stop();
            millisecondsLabel.Text = _stopwatch.ElapsedMilliseconds.ToString();

            if (serverResponse.Success)
                MessagesListBox.Items.Add(pushMessageTextBox.Text);
            else
                MessageBox.Show(serverResponse.ErrorDetails, "Error");
        }

        private async void getMessageButton_Click(object sender, EventArgs e)
        {
            int index;
            if (!int.TryParse(IndexTextBox.Text, out index))
            {
                return;
            }

            _stopwatch.Reset();
            _stopwatch.Start();

            var serverResponse = await _selectedMessengerService.GetMessage(index);

            _stopwatch.Stop();
            millisecondsLabel.Text = _stopwatch.ElapsedMilliseconds.ToString();

            if (serverResponse.Success)
                MessageBox.Show(serverResponse.Content.ToString());
            else
                MessageBox.Show(serverResponse.ErrorDetails, "Error");
        }

        private async void getAllMessagesButton_Click(object sender, EventArgs e)
        {
            _stopwatch.Reset();
            _stopwatch.Start();

            var serverResponse = await _selectedMessengerService.GetAllMessages();

            _stopwatch.Stop();
            millisecondsLabel.Text = _stopwatch.ElapsedMilliseconds.ToString();

            if (serverResponse.Success)
                DisplayMessages(serverResponse.Content);
            else
                MessageBox.Show(serverResponse.ErrorDetails, "Error");
        }

        private void webApi2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (IsRadioButtonChecked(sender))
                _selectedMessengerService = _webApi2MessengerService;
        }

        private void wcfRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (IsRadioButtonChecked(sender))
                _selectedMessengerService = _wcfMessengerService;
        }

        private async void deleteSelectedMessageButton_Click(object sender, EventArgs e)
        {
            if (MessagesListBox.SelectedItem == null)
                return;

            _stopwatch.Reset();
            _stopwatch.Start();

            var serverResponse = await _selectedMessengerService.DeleteMessage(MessagesListBox.SelectedIndex);

            _stopwatch.Stop();
            millisecondsLabel.Text = _stopwatch.ElapsedMilliseconds.ToString();

            if (serverResponse.Success)
                MessagesListBox.Items.RemoveAt(MessagesListBox.SelectedIndex);
            else
                MessageBox.Show(serverResponse.ErrorDetails, "Error");
        }

        private async void deleteAllMessagesButton_Click(object sender, EventArgs e)
        {
            _stopwatch.Reset();
            _stopwatch.Start();

            var serverResponse = await _selectedMessengerService.DeleteAllMessages();

            _stopwatch.Stop();
            millisecondsLabel.Text = _stopwatch.ElapsedMilliseconds.ToString();

            if (serverResponse.Success)
                MessagesListBox.Items.Clear();
            else
                MessageBox.Show(serverResponse.ErrorDetails, "Error");
        }

        private async void updateMessageButton_Click(object sender, EventArgs e)
        {
            if (MessagesListBox.SelectedIndex == -1)
                return;

            var newText = updateMessageTextBox.Text;
            var selectedMessageId = MessagesListBox.SelectedIndex;

            _stopwatch.Reset();
            _stopwatch.Start();

            var serverResponse = await _selectedMessengerService.UpdateMessage(selectedMessageId, newText);

            _stopwatch.Stop();
            millisecondsLabel.Text = _stopwatch.ElapsedMilliseconds.ToString();

            if (serverResponse.Success)
                MessagesListBox.Items[selectedMessageId] = newText;
            else
                MessageBox.Show(serverResponse.ErrorDetails, "Error");
        }

        private async void uploadButton_Click(object sender, EventArgs e)
        {
            uploadButton.Enabled = false;

            int size = (int)uploadNumericUpDown.Value;

            var garbageData = new MemoryStream( CreateGarbageData(size) );

            _stopwatch.Reset();
            _stopwatch.Start();

            var serverResponse = await _selectedMessengerService.UploadGarbageData(garbageData);

            _stopwatch.Stop();
            millisecondsLabel.Text = _stopwatch.ElapsedMilliseconds.ToString();
            garbageData.Dispose();

            uploadButton.Enabled = true;

            if (!serverResponse.Success)
                MessageBox.Show(serverResponse.ErrorDetails, "Error");
        }

        private byte[] CreateGarbageData(int megabytes)
        {
            int numberOfBytesInAMegabyte = 1024 * 1024;
            int numberOfBytes = numberOfBytesInAMegabyte * megabytes;

            var garbageData = new byte[numberOfBytes];

            for (int i = 0; i < numberOfBytes; i++)
                garbageData[i] = new byte();

            return garbageData;
        }
    }
}
