using ChatLibrary;
using System.ServiceModel;

namespace Client
{
    public class ServerChatProxy : DuplexClientBase<IServerChatService>
    {
        public ServerChatProxy(IClientChatService callbackInstance) : base(callbackInstance)
        { }
    }
}
