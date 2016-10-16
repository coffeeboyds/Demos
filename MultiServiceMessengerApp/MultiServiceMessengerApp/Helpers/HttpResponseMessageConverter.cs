using Common.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace MultiServiceMessengerApp.Helpers
{
    // Converts a HttpResponseMessage object to my custom ServerResponse<T> object.
    public static class HttpResponseMessageConverter<T>
    {
        public async static Task<ServerResponse<T>> ConvertToServerResponse(HttpResponseMessage httpMessageResponse)
        {
            if (httpMessageResponse.IsSuccessStatusCode)
            {
                return new ServerResponse<T>
                {
                    Success = true,
                    Content = await httpMessageResponse.Content.ReadAsAsync<T>()
                };
            }
            else
            {
                return new ServerResponse<T>
                {
                    Success = false,
                    ErrorDetails = httpMessageResponse.ReasonPhrase
                };
            }
        }
    }

    public static class HttpResponseMessageConverter
    {
        public static ServerResponse ConvertToServerResponse(HttpResponseMessage httpMessageResponse)
        {
            if (httpMessageResponse.IsSuccessStatusCode)
            {
                return new ServerResponse
                {
                    Success = true
                };
            }
            else
            {
                return new ServerResponse
                {
                    Success = false,
                    ErrorDetails = httpMessageResponse.ReasonPhrase
                };
            }
        }
    }
}
