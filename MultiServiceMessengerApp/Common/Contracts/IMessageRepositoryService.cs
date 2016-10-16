using Common.Models;
using System.Collections.Generic;

namespace Common.Contracts
{
    public interface IMessageRepositoryService
    {
        void CreateMessage(Message message);
        Message GetMessage(int id);
        List<Message> GetAllMessages();
        bool IsIdExist(int id);
        void DeleteAllMessages();
        void DeleteMessage(int id);
        void UpdateMessage(int id, string newMessage);
    }
}
