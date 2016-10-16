using Common.Contracts;
using Common.Models;
using Common.Services;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestMessengerService.Controllers
{
    [RoutePrefix("api/messenger/message")]
    public class MessengerController : ApiController
    {
        private IMessageRepositoryService _messengerRepository;

        public MessengerController()
        {
            _messengerRepository = MessageRepositoryService.Instance;
        }

        [NonAction]
        private HttpResponseMessage CreateMessageNotFoundResponse(int id)
        {
            var response = Request.CreateResponse(HttpStatusCode.NotFound);

            response.Content = new StringContent(string.Format("No Message with ID = {0}", id));
            response.ReasonPhrase = "Message ID not found";

            return response;
        }

        [HttpGet]
        [Route(Name="GetAllMessagesRoute")]
        public HttpResponseMessage GetAllMessages()
        {
            try
            {
                var messages = _messengerRepository.GetAllMessages();

                return Request.CreateResponse(HttpStatusCode.OK, messages);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("{id:int}", Name="GetMessageRoute")]
        public HttpResponseMessage GetMessage([FromUri]int id)
        {
            try
            {
                if (_messengerRepository.IsIdExist(id))
                {
                    var message = _messengerRepository.GetMessage(id);

                    return Request.CreateResponse(HttpStatusCode.OK, message);
                }
                else
                {
                    return CreateMessageNotFoundResponse(id);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route(Name="CreateMessageRoute")]
        public HttpResponseMessage CreateMessage([FromBody]Message message)
        {
            try
            {
                _messengerRepository.CreateMessage(message);

                var response = Request.CreateResponse(HttpStatusCode.Created);
                
                var uri = Url.Link("GetMessageRoute", new { id = message.Id });
                response.Headers.Location = new Uri(uri);

                return response;
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("{uploadgarbagedata}", Name = "UploadGarbageDataRoute")]
        public HttpResponseMessage UploadGarbageData([FromBody]MemoryStream garbageData)
        {
            try
            {
                // Just dispose it and send OK back, this is only to test performance.
                garbageData.Dispose();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("{id:int}", Name="UpdateMessageRoute")]
        public HttpResponseMessage UpdateMessage([FromUri] int id, [FromBody]string message)
        {
            try
            {
                if (_messengerRepository.IsIdExist(id))
                {
                    _messengerRepository.UpdateMessage(id, message);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return CreateMessageNotFoundResponse(id);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("{id:int}", Name="DeleteMessageRoute")]
        public HttpResponseMessage DeleteMessage([FromUri]int id)
        {
            try
            {
                if (!_messengerRepository.IsIdExist(id))
                {
                    return CreateMessageNotFoundResponse(id);
                }
                
                _messengerRepository.DeleteMessage(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route(Name="DeleteAllMessagesRoute")]
        public HttpResponseMessage DeleteAllMessages()
        {
            try
            {
                _messengerRepository.DeleteAllMessages();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
