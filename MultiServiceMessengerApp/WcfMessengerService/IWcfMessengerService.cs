using Common.Models;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using WcfMessengerService.Attributes;
using WcfMessengerService.ErrorHandlers;

namespace WcfMessengerService
{
    [ServiceContract]
    public interface IWcfMessengerService
    {
        // Every method with the [OperationContract] annotation will be accessible
        // to the client.

        [OperationContract]
        ServerResponse CreateMessage(Message message);

        [OperationContract]
        ServerResponse<Message> GetMessage(int id);

        [OperationContract]
        ServerResponse<List<Message>> GetAllMessages();

        [OperationContract]
        ServerResponse UpdateMessage(int id, string message);

        [OperationContract]
        ServerResponse DeleteMessage(int id);

        [OperationContract]
        ServerResponse DeleteAllMessages();

        [OperationContract]
        ServerResponse UploadGarbageData(MemoryStream garbageData);
    }
}
