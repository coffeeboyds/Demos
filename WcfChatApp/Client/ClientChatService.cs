using ChatLibrary;
using System;
using System.Collections.Generic;

namespace Client
{
    public class ClientChatService : IClientChatService
    {
        public delegate void NewMessageReceivedDelegate(string name);
        public event NewMessageReceivedDelegate NewMessageReceivedEvent;

        public delegate void ConnectedClientsListUpdatedDelegate(List<ConnectedClient> connectedClients);
        public event ConnectedClientsListUpdatedDelegate ConnectedClientsListUpdatedEvent;

        public void ReceiveNewMessage(string message)
        {
            NewMessageReceivedEvent(message);
        }

        public void UpdateListOfConnectedClients(List<ConnectedClient> connectedClients)
        {
            ConnectedClientsListUpdatedEvent(connectedClients);
        }
    }
}
