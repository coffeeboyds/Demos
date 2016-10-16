using Common.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WcfMessengerService;

namespace MultiServiceMessengerApp.Contracts
{
    public interface IAsyncMessengerService
    {
        Task<ServerResponse<List<Message>>> GetAllMessages();
        Task<ServerResponse<Message>> GetMessage(int id);
        Task<ServerResponse> CreateMessage(Message message);
        Task<ServerResponse> UpdateMessage(int id, string newMessage);
        Task<ServerResponse> DeleteMessage(int id);
        Task<ServerResponse> DeleteAllMessages();
        Task<ServerResponse> UploadGarbageData(MemoryStream garbageData);
    }
}
