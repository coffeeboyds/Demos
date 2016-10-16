using System.Collections.Generic;
using System.ServiceModel;

namespace ChatLibrary
{
    // This is the callback contract for IServerChatService.
    // So the client (the application that uses IServerChatService) implements it.
    // It defines how IServerChatService can signal its clients.

    [ServiceContract]
    public interface IClientChatService
    {
        // Setting IsOneWay=true tells the server to
        // continue as soon as the outbound data is written to the network connection,
        // making it a non-blocking call (unless a network problem ensues). 
        // Operations with IsOneWay=true can only return void.

        [OperationContract(IsOneWay = true)]
        void UpdateListOfConnectedClients(List<ConnectedClient> connectedClients);

        [OperationContract(IsOneWay = true)]
        void ReceiveNewMessage(string message);
    }
}
