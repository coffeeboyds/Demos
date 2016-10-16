using System;
using System.ServiceModel;

namespace ChatLibrary
{
    [ServiceContract(CallbackContract=typeof(IClientChatService))]
    public interface IServerChatService
    {
        // Setting IsOneWay=true tells the client application to
        // continue as soon as the outbound data is written to the network connection,
        // making it a non-blocking call (unless a network problem ensues). 
        // Operations with IsOneWay=true can only return void.

        [OperationContract(IsOneWay = true)]
        void SendMessage(Guid clientId, Guid recipientId, string message);

        [OperationContract(IsOneWay = true)]
        void Logon(Guid clientId, string userName);

        [OperationContract(IsOneWay = true)]
        void Logoff(Guid clientId);
    }
}
