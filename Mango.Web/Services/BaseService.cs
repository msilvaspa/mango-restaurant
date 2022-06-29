using System.Text;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;

namespace Mango.Web.Services;

public class BaseService: IBaseService
{
    public ResponseDto responseModel { get; set; }
    public IHttpClientFactory _httpClient { get; set; }

    public BaseService(IHttpClientFactory httpClient)
    {
        this._httpClient = httpClient;
    }
    
    
    public async Task<T> SendAsync<T>(ApiRequest request)
    {
        try
        {
            var client = _httpClient.CreateClient("MangoApi");
            var message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(request.Url);
            client.DefaultRequestHeaders.Clear();
            if (request.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage apiResponse = null;

            switch (request.ApiType)
            {
                case SD.ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;               
                case SD.ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;        
                case SD.ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;       
                default:
                    message.Method = HttpMethod.Get;
                    break;
                
            }
            apiResponse = await client.SendAsync(message);
            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
            return apiResponseDto;
        }
        catch (Exception e)
        {
            var dto = new ResponseDto()
            {
                DisplayMessage = "Error",
                IsSuccess = false,
                ErrorMessage = new List<string>() { Convert.ToString(e.Message) }
            };
            var res = JsonConvert.SerializeObject(dto);
            return JsonConvert.DeserializeObject<T>(res);
        }
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }
}