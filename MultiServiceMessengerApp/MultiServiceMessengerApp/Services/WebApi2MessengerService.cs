using Common.Models;
using MultiServiceMessengerApp.Contracts;
using MultiServiceMessengerApp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MultiServiceMessengerApp.Services
{
    public class WebApi2MessengerService : IAsyncMessengerService
    {
        private const string BaseAddress = "http://localhost:58050/";

        private void InitializeClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(BaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ServerResponse> DeleteAllMessages()
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);

                var response = await client.DeleteAsync("api/messenger/message");

                return HttpResponseMessageConverter.ConvertToServerResponse(response);
            }
        }

        public async Task<ServerResponse> DeleteMessage(int id)
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);

                var response = await client.DeleteAsync(string.Format("api/messenger/message/{0}", id));

                return HttpResponseMessageConverter.ConvertToServerResponse(response);
            }
        }

        public async Task<ServerResponse<List<Message>>> GetAllMessages()
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);

                var response = await client.GetAsync("api/messenger/message");
                
                return await HttpResponseMessageConverter<List<Message>>.ConvertToServerResponse(response);
            }
        }

        public async Task<ServerResponse<Message>> GetMessage(int id)
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);

                var response = await client.GetAsync(string.Format("api/messenger/message/{0}", id));

                return await HttpResponseMessageConverter<Message>.ConvertToServerResponse(response);
            }
        }

        public async Task<ServerResponse> CreateMessage(Message message)
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);

                var response = await client.PostAsJsonAsync("api/messenger/message", message);

                return HttpResponseMessageConverter.ConvertToServerResponse(response);
            }
        }

        public async Task<ServerResponse> UpdateMessage(int id, string newMessage)
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);

                var response = await client.PutAsJsonAsync(string.Format("api/messenger/message/{0}", id), newMessage);

                return HttpResponseMessageConverter.ConvertToServerResponse(response);
            }
        }

        public async Task<ServerResponse> UploadGarbageData(MemoryStream garbageData)
        {
            using (var client = new HttpClient())
            {
                InitializeClient(client);

                var response = await client.PostAsJsonAsync(string.Format("api/messenger/message/uploadgarbagedata"), garbageData);

                return HttpResponseMessageConverter.ConvertToServerResponse(response);
            }
        }
    }
}
