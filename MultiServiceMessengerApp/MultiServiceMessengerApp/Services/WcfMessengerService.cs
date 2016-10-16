using Common.Models;
using MultiServiceMessengerApp.Contracts;
using MultiServiceMessengerApp.WcfServiceReference;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace MultiServiceMessengerApp.Services
{
    // This class uses the Task<T>.Factory.FromAsync method with the generated
    // asynchronous methods from WCF so that the service can be used with the async/await pattern.
    public class WcfMessengerService : IAsyncMessengerService
    {
        public IWcfMessengerService _wcfMessengerServiceClient;

        public WcfMessengerService()
        {
            _wcfMessengerServiceClient = new WcfMessengerServiceClient();
        }

        public Task<ServerResponse> DeleteAllMessages()
        {
            return Task<ServerResponse>.Factory.FromAsync( _wcfMessengerServiceClient.BeginDeleteAllMessages,
                                                           _wcfMessengerServiceClient.EndDeleteAllMessages,
                                                           null);
        }

        public Task<ServerResponse> DeleteMessage(int id)
        {
            return Task<ServerResponse>.Factory.FromAsync( _wcfMessengerServiceClient.BeginDeleteMessage,
                                                           _wcfMessengerServiceClient.EndDeleteMessage,
                                                           id, null);
        }

        public Task<ServerResponse<List<Message>>> GetAllMessages()
        {
            return Task<ServerResponse<List<Message>>>.Factory.FromAsync(_wcfMessengerServiceClient.BeginGetAllMessages,
                                                                         _wcfMessengerServiceClient.EndGetAllMessages,
                                                                         null);
        }

        public Task<ServerResponse<Message>> GetMessage(int id)
        {
            return Task<ServerResponse<Message>>.Factory.FromAsync( _wcfMessengerServiceClient.BeginGetMessage,
                                                                    _wcfMessengerServiceClient.EndGetMessage,
                                                                    id, null);
        }

        public Task<ServerResponse> CreateMessage(Message message)
        {
            return Task.Factory.FromAsync( _wcfMessengerServiceClient.BeginCreateMessage,
                                           _wcfMessengerServiceClient.EndCreateMessage,
                                           message, null);
        }

        public Task<ServerResponse> UpdateMessage(int id, string newMessage)
        {
            return Task.Factory.FromAsync(_wcfMessengerServiceClient.BeginUpdateMessage,
                                          _wcfMessengerServiceClient.EndUpdateMessage,
                                          id, newMessage, null);
        }

        public Task<ServerResponse> UploadGarbageData(MemoryStream garbageData)
        {
            return Task.Factory.FromAsync(_wcfMessengerServiceClient.BeginUploadGarbageData,
                                          _wcfMessengerServiceClient.EndUploadGarbageData,
                                          garbageData, null);
        }
    }
}
