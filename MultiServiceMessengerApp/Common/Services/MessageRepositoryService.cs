using Common.Contracts;
using Common.Models;
using System.Collections.Generic;
using System;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace Common.Services
{
    // Used by both the Web API 2 and WCF services
    // It's class that reads/writes the messages to a mapped memory location
    // so that both services can share the same messages on a localhost.
    // It's irrelevant to either service so you probably are not interested in this code. 
    public sealed class MessageRepositoryService : IMessageRepositoryService, IDisposable
    {
        private const char Delimiter = '\0';

        private static MessageRepositoryService _instance;
        private MemoryMappedFile _pagedMemoryMappedMessages;
        private MemoryMappedFile _pagedMemoryMappedMessagesSize;
        private Mutex _mutex;

        private MessageRepositoryService()
        {
            _mutex = new Mutex(false, "MessengerMutex");

            MemoryMappedFileSecurity CustomSecurity = new MemoryMappedFileSecurity();

            CustomSecurity.AddAccessRule(new System.Security.AccessControl.AccessRule<MemoryMappedFileRights>("everyone", MemoryMappedFileRights.FullControl, System.Security.AccessControl.AccessControlType.Allow));

            _pagedMemoryMappedMessages = MemoryMappedFile.CreateOrOpen(
                   @"Messages",                                    // Name
                   1024 * 1024,                                    // Size
                   MemoryMappedFileAccess.ReadWrite,               // Access type
                   MemoryMappedFileOptions.DelayAllocatePages,     // Pseudo reserve/commit
                   CustomSecurity,                                 // You can customize the security
                   HandleInheritability.Inheritable);              // Inherit to child process

            _pagedMemoryMappedMessagesSize = MemoryMappedFile.CreateOrOpen(
                    @"MessagesSize",                                // Name
                    32,                                             // Size
                    MemoryMappedFileAccess.ReadWrite,               // Access type
                    MemoryMappedFileOptions.DelayAllocatePages,     // Pseudo reserve/commit
                    CustomSecurity,                                 // You can customize the security
                    HandleInheritability.Inheritable);              // Inherit to child process
        }

        public static MessageRepositoryService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MessageRepositoryService();

                return _instance;
            }
        }
        
        public List<Message> GetAllMessages()
        {
            string messages = string.Empty;

            if (_mutex.WaitOne())
            {
                try
                {
                    int size;

                    using (MemoryMappedViewAccessor fileMap = _pagedMemoryMappedMessagesSize.CreateViewAccessor())
                    {
                        fileMap.Read(0, out size);
                    }

                    using (var stream = _pagedMemoryMappedMessages.CreateViewStream())
                    {
                        var textReader = new StreamReader(stream);
                        messages = textReader.ReadToEnd().Substring(0, size);
                    }
                }
                finally { _mutex.ReleaseMutex(); }
            }

            var messageTextList = new List<string>(messages.Split(Delimiter));

            messageTextList.RemoveAt(messageTextList.Count - 1); // Remove the last item because it is an empty string

            var messageList = new List<Message>();

            int id = 0;
            foreach (var messageText in messageTextList)
            {
                messageList.Add(new Message
                {
                    Id = id++,
                    Text = messageText
                });
            }

            return messageList;
        }

        public Message GetMessage(int id)
        {
            var messages = GetAllMessages();

            return messages.Count() > id
                                ? messages.ElementAt(id)
                                : null;
        }

        public void CreateMessage(Message message)
        {
            if (_mutex.WaitOne())
            {
                try
                {
                    int size;

                    using (MemoryMappedViewAccessor fileMap = _pagedMemoryMappedMessagesSize.CreateViewAccessor())
                    {
                        fileMap.Read(0, out size);
                    }

                    using (var stream = _pagedMemoryMappedMessages.CreateViewStream())
                    {
                        var textInBytes = Encoding.ASCII.GetBytes(message.Text + Delimiter);
                
                        stream.Seek(size, SeekOrigin.Begin);
                        stream.Write(textInBytes, 0, textInBytes.Length);
                    }

                    using (MemoryMappedViewAccessor fileMap = _pagedMemoryMappedMessagesSize.CreateViewAccessor())
                    {
                        fileMap.Write(0, size + message.Text.Length + 1); // Plus 1 for the Delimiter
                    }

                }
                finally { _mutex.ReleaseMutex(); }
            }
        }
        
        public bool IsIdExist(int id)
        {
            int size;

            using (MemoryMappedViewAccessor fileMap = _pagedMemoryMappedMessagesSize.CreateViewAccessor())
            {
                fileMap.Read(0, out size);
            }

            string messages;

            using (var stream = _pagedMemoryMappedMessages.CreateViewStream())
            {
                var textReader = new StreamReader(stream);
                messages = textReader.ReadToEnd().Substring(0, size);
            }

            return messages.Where(c => c == Delimiter).Count() > id;
        }

        public void DeleteAllMessages()
        {
            if (_mutex.WaitOne())
            {
                try
                {
                    using (var fileMap = _pagedMemoryMappedMessagesSize.CreateViewAccessor())
                    {
                        fileMap.Write(0, 0); // Set to size to 0 so it doesn't hang around in memory
                    }

                    using (var stream = _pagedMemoryMappedMessagesSize.CreateViewAccessor())
                    {
                        stream.Flush();
                    }

                    using (var stream = _pagedMemoryMappedMessages.CreateViewAccessor())
                    {
                        stream.Flush();
                    }
                }
                finally { _mutex.ReleaseMutex(); }
            }
        }

        public void Dispose()
        {
            if (_pagedMemoryMappedMessages != null)
            {
                _pagedMemoryMappedMessages.Dispose();
            }

            if (_pagedMemoryMappedMessagesSize != null)
            {
                _pagedMemoryMappedMessagesSize.Dispose();
            }
        }

        public void DeleteMessage(int id)
        {
            if (_mutex.WaitOne())
            {
                try
                {
                    var messages = GetAllMessages();

                    messages.RemoveAt(id);

                    DeleteAllMessages();

                    foreach (var message in messages)
                        CreateMessage(message);
                }
                finally { _mutex.ReleaseMutex(); }
            }
        }

        public void UpdateMessage(int id, string newMessage)
        {
            if (_mutex.WaitOne())
            {
                try
                {
                    if (!IsIdExist(id))
                        return;

                    var messages = GetAllMessages();

                    messages[id].Text = newMessage;

                    DeleteAllMessages();

                    foreach (var message in messages)
                        CreateMessage(message);
                }
                finally { _mutex.ReleaseMutex(); }
            }
        }
    }
}
