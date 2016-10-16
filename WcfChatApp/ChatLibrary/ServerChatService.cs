using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace ChatLibrary
{
    // InstanceContextMode.Single makes sure the same service is being used by all clients.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServerChatService : IServerChatService
    {
        private List<ConnectedClient> _connectedClients = new List<ConnectedClient>();

        public delegate void RefreshListOfNames(List<string> names);
        public static event RefreshListOfNames UpdateListOfConnectedClients;

        public void Logoff(Guid clientId)
        {
            var loggedOffClient = _connectedClients.FirstOrDefault(c => c.Id == clientId);
            
            _connectedClients.Remove(loggedOffClient);

            foreach (var client in _connectedClients)
                client.CallbackChannel.UpdateListOfConnectedClients(_connectedClients);

            UpdateListOfConnectedClients(_connectedClients.Select(c => c.Username).ToList());
        }

        public void Logon(Guid clientId, string userName)
        {
            _connectedClients.Add(new ConnectedClient
            {
                Id = clientId,
                Username = userName,
                CallbackChannel = OperationContext.Current.GetCallbackChannel<IClientChatService>()
            });

            // We're calling the callback for all the connected
            // clients to update their list of connected clients.
            // It is important that this call is marked as IsOneWay=true
            // Otherwise, a deadlock would occur since this service is locked when in use.
            foreach (var client in _connectedClients)
                client.CallbackChannel.UpdateListOfConnectedClients(_connectedClients);

            UpdateListOfConnectedClients(_connectedClients.Select(c => c.Username).ToList());
        }

        public void SendMessage(Guid clientId, Guid recipientId, string message)
        {
            var sender = _connectedClients.FirstOrDefault(c => c.Id == clientId);
            var recipient = _connectedClients.FirstOrDefault(c => c.Id == recipientId);

            if(recipient != null)
            {
                recipient.CallbackChannel.ReceiveNewMessage(string.Format("{0}>{1}", sender.Username, message));
            }
        }
    }
}
