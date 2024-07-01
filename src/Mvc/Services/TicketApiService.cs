using System.Text.Json;
using Mvc.Models;
using Mvc.Models.Task;
using Mvc.Models.Ticket;
using Mvc.Validators;

namespace Mvc.Services;

public class TicketApiService(
    IHttpContextAccessor httpContextAccessor,
    IHttpClientFactory httpClientFactory)
{
    public async Task<ApiDataResponse<List<GetAllTicketDto>>?> GetAllAsync()
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
        
        if (accessToken == null)
        {
            return new ApiDataResponse<List<GetAllTicketDto>> { Success = false, Message = "Tekrar giriş yapınız." };
        }
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7001/api/Tickets/GetAll")
            {
                Headers =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };
            
            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<ApiDataResponse<List<GetAllTicketDto>>>();
                if (responseBody!.Success)
                    return responseBody;
                return new ApiDataResponse<List<GetAllTicketDto>> { Data = responseBody.Data, Success = responseBody.Success, Message = responseBody.Message };
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new ApiDataResponse<List<GetAllTicketDto>> { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new ApiDataResponse<List<GetAllTicketDto>> { Success = false, Message = "Mesajlar listelenirken hata: " + ex.Message };
        }
    }
    
    public async Task<ApiDataResponse<List<GetByTicketIdDto>>> GetDetailsAsync(Guid ticketId)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiDataResponse<List<GetByTicketIdDto>> { Success = false, Message = "Tekrar giriş yapınız." };
        
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Tickets/GetDetails")
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(new { ticketId = ticketId.ToString() }), 
                    System.Text.Encoding.UTF8, 
                    "application/json"),
                Headers =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };

            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<ApiDataResponse<List<GetByTicketIdDto>>>();
                if (responseBody!.Success)
                    return responseBody;
                else
                    return new ApiDataResponse<List<GetByTicketIdDto>> { Success = false, Message = responseBody.Message };
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new ApiDataResponse<List<GetByTicketIdDto>> { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new ApiDataResponse<List<GetByTicketIdDto>> { Success = false, Message = "Mesajları getirme sırasında hata: " + ex.Message };
        }
    }
    
    public async Task<ApiResponse?> CreateAsync(CreateTicketDto createTicketDto)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        var createTicketValidator = new CreateTicketDtoValidator();
        var validationResult = await createTicketValidator.ValidateAsync(createTicketDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return new ApiResponse { Success = false, Message = errors.FirstOrDefault()! };
        }

        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Tickets/Create")
            {
                Content = JsonContent.Create(createTicketDto),
                Headers =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };

            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<ApiResponse>();
                if (responseBody!.Success)
                    return responseBody;
                else
                    return new ApiResponse { Success = false, Message = responseBody.Message };
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new ApiResponse { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse { Success = false, Message = "Mesaj sırasında hata: " + ex.Message };
        }
    }
    
    public async Task<ApiResponse?> CreateDetailContentAsync(CreateTicketDetailDto createTicketDetailDto)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        var createTicketDetailValidator = new CreateTicketDetailDtoValidator();
        var validationResult = await createTicketDetailValidator.ValidateAsync(createTicketDetailDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return new ApiResponse { Success = false, Message = errors.FirstOrDefault()! };
        }

        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Tickets/CreateDetailContent")
            {
                Content = JsonContent.Create(createTicketDetailDto),
                Headers =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };

            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<ApiResponse>();
                if (responseBody!.Success)
                    return responseBody;
                else
                    return new ApiResponse { Success = false, Message = responseBody.Message };
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new ApiResponse { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse { Success = false, Message = "Mesaj sırasında hata: " + ex.Message };
        }
    }
    
    public async Task<ApiDataResponse<List<GetByTicketIdDto>>> GetByIdAsync(Guid ticketId)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiDataResponse<List<GetByTicketIdDto>> { Success = false, Message = "Tekrar giriş yapınız." };
        
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Tickets/GetById")
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(new { ticketId = ticketId.ToString() }), 
                    System.Text.Encoding.UTF8, 
                    "application/json"),
                Headers =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };

            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<ApiDataResponse<List<GetByTicketIdDto>>>();
                if (responseBody!.Success)
                    return responseBody;
                else
                    return new ApiDataResponse<List<GetByTicketIdDto>> { Success = false, Message = responseBody.Message };
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new ApiDataResponse<List<GetByTicketIdDto>> { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new ApiDataResponse<List<GetByTicketIdDto>> { Success = false, Message = "Mesajları getirme sırasında hata: " + ex.Message };
        }
    }
}