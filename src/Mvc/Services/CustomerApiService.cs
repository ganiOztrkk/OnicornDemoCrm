using Mvc.Models;

namespace Mvc.Services;

public class CustomerApiService(
    IHttpContextAccessor httpContextAccessor,
    IHttpClientFactory httpClientFactory)
{
    public async Task<GetAllCustomerApiResponse?> GetAllAsync()
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
        {
            return new GetAllCustomerApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        }
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7001/api/Customers/GetAll")
            {
                Headers =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };
            
            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<GetAllCustomerApiResponse>();
                if (responseBody!.Success)
                    return responseBody;
                return new GetAllCustomerApiResponse { Data = responseBody.Data, Success = responseBody.Success, Message = responseBody.Message };
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new GetAllCustomerApiResponse { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new GetAllCustomerApiResponse { Success = false, Message = "Exception in CreateVehicleAsync: " + ex.Message };
        }
    }

    public async Task<CreateCustomerApiResponse?> CreateAsync(CreateCustomerDto createCustomerDto)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
        {
            return new CreateCustomerApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        }
        
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Customers/Create")
            {
                Content = JsonContent.Create(createCustomerDto),
                Headers =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };

            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<CreateCustomerApiResponse>();
                return responseBody;
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new CreateCustomerApiResponse { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new CreateCustomerApiResponse { Success = false, Message = "Müşteri kayıt sırasında hata: " + ex.Message };
        }
    }
}
public class GetAllCustomerApiResponse
{
    public List<GetAllCustomerDto>? Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class CreateCustomerApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}