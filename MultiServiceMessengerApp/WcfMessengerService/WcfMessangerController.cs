using Common.Contracts;
using Common.Models;
using Common.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using WcfMessengerService.Attributes;
using WcfMessengerService.ErrorHandlers;

namespace WcfMessengerService
{
    [ErrorBehavior(typeof(MessengerErrorHandler))]
    public class WcfMessengerController : IWcfMessengerService
    {
        private IMessageRepositoryService _messengerService;

        public WcfMessengerController()
        {
            _messengerService = MessageRepositoryService.Instance;
        }
        
        public ServerResponse<List<Message>> GetAllMessages()
        {
            try
            {
                return new ServerResponse<List<Message>>
                {
                    Success = true,
                    Content = _messengerService.GetAllMessages()
                };
            }
            catch(Exception ex)
            {
                return new ServerResponse<List<Message>>
                {
                    Success = false,
                    ErrorDetails = ex.Message
                };
            }
        }

        public ServerResponse<Message> GetMessage(int id)
        {
            try
            {
                var idExists = _messengerService.IsIdExist(id);
                Message message = null;
                string errorDetails = null;

                if (idExists)
                    message = _messengerService.GetMessage(id);
                else
                    errorDetails = "Message ID not found.";


                return new ServerResponse<Message>
                {
                    Success = idExists && message != null,
                    Content = message,
                    ErrorDetails = errorDetails
                };
            }
            catch (Exception ex)
            {
                return new ServerResponse<Message>
                {
                    Success = false,
                    ErrorDetails = ex.Message
                };
            }
        }

        public ServerResponse CreateMessage(Message message)
        {
            // This is how error handling is done with WCF.
            // We throw an exception back to the client.
            // I don't like this way, I'd rather wrap everything in a try/catch and return a ServerResponse object (like I do in all my other methods).
            if (string.IsNullOrWhiteSpace(message.Text) ||
                message.Text.Length > 20)
                throw new FaultException("The message must be between 1 and 20 characters.");

            _messengerService.CreateMessage(message);

            return new ServerResponse { Success = true };
        }

        public ServerResponse UpdateMessage(int id, string message)
        {
            try
            {
                _messengerService.UpdateMessage(id, message);

                return new ServerResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new ServerResponse
                {
                    Success = false,
                    ErrorDetails = ex.Message
                };
            }
        }

        public ServerResponse DeleteMessage(int id)
        {
            try
            {
                _messengerService.DeleteMessage(id);

                return new ServerResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new ServerResponse
                {
                    Success = false,
                    ErrorDetails = ex.Message
                };
            }
        }

        public ServerResponse DeleteAllMessages()
        {
            try
            {
                _messengerService.DeleteAllMessages();

                return new ServerResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new ServerResponse
                {
                    Success = false,
                    ErrorDetails = ex.Message
                };
            }
        }

        public ServerResponse UploadGarbageData(MemoryStream garbageData)
        {
            // Just dispose it and send success back, this is only to test performance.
            garbageData.Dispose();

            return new ServerResponse
            {
                Success = true
            };
        }
    }
}
