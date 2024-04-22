using System.Net;
using System.Text;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Mango.Web.Service
{
    public class BaseService(IHttpClientFactory clientFactory) : IBaseService
    {
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                var client = clientFactory.CreateClient("MangoApi");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(
                        JsonSerializer.Serialize(requestDto.Data),
                        Encoding.UTF8, "application/json");
                }

                message.Method = requestDto.ApiType switch
                {
                    Sd.ApiType.Post => HttpMethod.Post,
                    Sd.ApiType.Put => HttpMethod.Put,
                    Sd.ApiType.Delete => HttpMethod.Delete,
                    _ => HttpMethod.Get
                };

                var apiResponse = await client.SendAsync(message);
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDto { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new ResponseDto { IsSuccess = false, Message = "Forbidden" };
                    case HttpStatusCode.Unauthorized:
                        return new ResponseDto { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new ResponseDto { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception e)
            {
                return new ResponseDto { IsSuccess = false, Message = e.Message };
            }
            
        }
    }
}
