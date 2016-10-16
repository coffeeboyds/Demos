using System;
using System.Runtime.Serialization;

namespace ChatLibrary
{
    [DataContract]
    public class ConnectedClient
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Username { get; set; }

        [IgnoreDataMember]
        public IClientChatService CallbackChannel { get; set; }
    }
}
