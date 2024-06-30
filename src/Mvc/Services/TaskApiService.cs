using System.Text.Json;
using Mvc.Models;
using Mvc.Models.Task;
using Mvc.Models.User;
using Mvc.Validators;

namespace Mvc.Services;

public class TaskApiService(
    IHttpContextAccessor httpContextAccessor,
    IHttpClientFactory httpClientFactory)
{
    public async Task<ApiDataResponse<List<GetAllTaskDto>>?> GetAllAsync()
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
        
        if (accessToken == null)
        {
            return new ApiDataResponse<List<GetAllTaskDto>> { Success = false, Message = "Tekrar giriş yapınız." };
        }
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7001/api/Tasks/GetAll")
            {
                Headers =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };
            
            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<ApiDataResponse<List<GetAllTaskDto>>>();
                if (responseBody!.Success)
                    return responseBody;
                return new ApiDataResponse<List<GetAllTaskDto>> { Data = responseBody.Data, Success = responseBody.Success, Message = responseBody.Message };
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new ApiDataResponse<List<GetAllTaskDto>> { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new ApiDataResponse<List<GetAllTaskDto>> { Success = false, Message = "Görevler listelenirken hata: " + ex.Message };
        }
    }
    
    public async Task<ApiResponse?> CreateAsync(CreateTaskDto createTaskDto)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        var createTaskValidator = new CreateTaskDtoValidator();
        var validationResult = await createTaskValidator.ValidateAsync(createTaskDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return new ApiResponse { Success = false, Message = errors.FirstOrDefault()! };
        }

        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Tasks/Create")
            {
                Content = JsonContent.Create(createTaskDto),
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
            return new ApiResponse { Success = false, Message = "Görev kayıt sırasında hata: " + ex.Message };
        }
    }
    
    public async Task<ApiDataResponse<List<GetTaskByUserIdDto>>> GetByIdAsync(Guid userId)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiDataResponse<List<GetTaskByUserIdDto>> { Success = false, Message = "Tekrar giriş yapınız." };
        
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Tasks/GetById")
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
                var responseBody = await response.Content.ReadFromJsonAsync<ApiDataResponse<List<GetTaskByUserIdDto>>>();
                if (responseBody!.Success)
                    return responseBody;
                else
                    return new ApiDataResponse<List<GetTaskByUserIdDto>> { Success = false, Message = responseBody.Message };
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new ApiDataResponse<List<GetTaskByUserIdDto>> { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new ApiDataResponse<List<GetTaskByUserIdDto>> { Success = false, Message = "Görevleri getirme sırasında hata: " + ex.Message };
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
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Tasks/Delete")
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(new { userId = id.ToString() }), 
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
            return new ApiResponse { Success = false, Message = "Görev silme sırasında hata: " + ex.Message };
        }
    }
    
    public async Task<ApiResponse?> UpdateAsync(UpdateTaskDto updateTaskDto)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        var updateTaskValidator = new UpdateTaskDtoValidator();
        var validationResult = await updateTaskValidator.ValidateAsync(updateTaskDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return new ApiResponse { Success = false, Message = errors.FirstOrDefault()! };
        }

        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Tasks/Update")
            {
                Content = JsonContent.Create(updateTaskDto),
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
            return new ApiResponse { Success = false, Message = "Görev güncelleme sırasında hata: " + ex.Message };
        }
    }
}