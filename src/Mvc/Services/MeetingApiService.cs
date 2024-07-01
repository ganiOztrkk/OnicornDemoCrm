using System.Text.Json;
using Mvc.Models;
using Mvc.Models.Meeting;
using Mvc.Validators;

namespace Mvc.Services;

public class MeetingApiService(
    IHttpContextAccessor httpContextAccessor,
    IHttpClientFactory httpClientFactory)
{
    public async Task<ApiDataResponse<List<GetAllMeetingDto>>?> GetAllAsync()
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
        
        if (accessToken == null)
        {
            return new ApiDataResponse<List<GetAllMeetingDto>> { Success = false, Message = "Tekrar giriş yapınız." };
        }
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7001/api/Meetings/GetAll")
            {
                Headers =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };
            
            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<ApiDataResponse<List<GetAllMeetingDto>>>();
                if (responseBody!.Success)
                    return responseBody;
                return new ApiDataResponse<List<GetAllMeetingDto>> { Data = responseBody.Data, Success = responseBody.Success, Message = responseBody.Message };
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new ApiDataResponse<List<GetAllMeetingDto>> { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new ApiDataResponse<List<GetAllMeetingDto>> { Success = false, Message = "Toplantılar listelenirken hata: " + ex.Message };
        }
    }
    
    public async Task<ApiResponse?> CreateAsync(CreateMeetingDto createMeetingDto)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        var createMeetingValidator = new CreateMeetingDtoValidator();
        var validationResult = await createMeetingValidator.ValidateAsync(createMeetingDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return new ApiResponse { Success = false, Message = errors.FirstOrDefault()! };
        }

        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Meetings/Create")
            {
                Content = JsonContent.Create(createMeetingDto),
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
            return new ApiResponse { Success = false, Message = "Toplantı kayıt sırasında hata: " + ex.Message };
        }
    }
    
    public async Task<ApiDataResponse<List<GetMeetingByUserIdDto>>> GetByIdAsync(Guid userId)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiDataResponse<List<GetMeetingByUserIdDto>> { Success = false, Message = "Tekrar giriş yapınız." };
        
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Meetings/GetByUserId")
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(new { userId = userId.ToString() }), 
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
                var responseBody = await response.Content.ReadFromJsonAsync<ApiDataResponse<List<GetMeetingByUserIdDto>>>();
                if (responseBody!.Success)
                    return responseBody;
                else
                    return new ApiDataResponse<List<GetMeetingByUserIdDto>> { Success = false, Message = responseBody.Message };
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new ApiDataResponse<List<GetMeetingByUserIdDto>> { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new ApiDataResponse<List<GetMeetingByUserIdDto>> { Success = false, Message = "Toplantıları getirme sırasında hata: " + ex.Message };
        }
    }
    
    public async Task<ApiResponse?> DeleteAsync(Guid id)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Meetings/Delete")
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(new { meetingId = id.ToString() }), 
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
            return new ApiResponse { Success = false, Message = "Toplantıyı silme sırasında hata: " + ex.Message };
        }
    }
    
    public async Task<ApiResponse?> UpdateAsync(UpdateMeetingDto updateMeetingDto)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        var updateMeetingValidator = new UpdateMeetingDtoValidator();
        var validationResult = await updateMeetingValidator.ValidateAsync(updateMeetingDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return new ApiResponse { Success = false, Message = errors.FirstOrDefault()! };
        }

        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Meetings/Update")
            {
                Content = JsonContent.Create(updateMeetingDto),
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
            return new ApiResponse { Success = false, Message = "Toplantı güncelleme sırasında hata: " + ex.Message };
        }
    }
}