using System.Text.Json;
using Mvc.Models;
using Mvc.Models.Customer;
using Mvc.Models.User;
using Mvc.Validators;

namespace Mvc.Services;

public class UserApiService(
    IHttpContextAccessor httpContextAccessor,
    IHttpClientFactory httpClientFactory)
{
    public async Task<ApiDataResponse<List<GetAllUserDto>>?> GetAllAsync()
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
        
        if (accessToken == null)
        {
            return new ApiDataResponse<List<GetAllUserDto>> { Success = false, Message = "Tekrar giriş yapınız." };
        }
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7001/api/Users/GetAll")
            {
                Headers =
                {
                    { "Authorization", $"Bearer {accessToken}" }
                }
            };
            
            var response = await client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<ApiDataResponse<List<GetAllUserDto>>>();
                if (responseBody!.Success)
                    return responseBody;
                return new ApiDataResponse<List<GetAllUserDto>> { Data = responseBody.Data, Success = responseBody.Success, Message = responseBody.Message };
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new ApiDataResponse<List<GetAllUserDto>> { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new ApiDataResponse<List<GetAllUserDto>> { Success = false, Message = "Kullanıcılar listelenirken hata: " + ex.Message };
        }
    }

    public async Task<ApiResponse?> CreateAsync(CreateUserDto createUserDto)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        var createUserValidator = new CreateUserDtoValidator();
        var validationResult = await createUserValidator.ValidateAsync(createUserDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return new ApiResponse { Success = false, Message = errors.FirstOrDefault()! };
        }

        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Customers/Create")
            {
                Content = JsonContent.Create(createUserDto),
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
            return new ApiResponse { Success = false, Message = "Müşteri kayıt sırasında hata: " + ex.Message };
        }
    }
    
    public async Task<ApiDataResponse<GetCustomerByIdDto>?> GetByIdAsync(Guid id)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiDataResponse<GetCustomerByIdDto> { Success = false, Message = "Tekrar giriş yapınız." };
        
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Customers/GetById")
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(new { id = id.ToString() }), 
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
                var responseBody = await response.Content.ReadFromJsonAsync<ApiDataResponse<GetCustomerByIdDto>>();
                if (responseBody!.Success)
                    return responseBody;
                else
                    return new ApiDataResponse<GetCustomerByIdDto> { Success = false, Message = responseBody.Message };
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return new ApiDataResponse<GetCustomerByIdDto> { Success = false, Message = "API Hatası: " + errorResponse };
            }
        }
        catch (Exception ex)
        {
            return new ApiDataResponse<GetCustomerByIdDto> { Success = false, Message = "Müşteri getirme sırasında hata: " + ex.Message };
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
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Customers/Delete")
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(new { id = id.ToString() }), 
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
            return new ApiResponse { Success = false, Message = "Müşteri kayıt sırasında hata: " + ex.Message };
        }
    }
    
    public async Task<ApiResponse?> UpdateAsync(UpdateCustomerDto updateCustomerDto)
    {
        var client = httpClientFactory.CreateClient();
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"];
            
        if (accessToken == null)
            return new ApiResponse { Success = false, Message = "Tekrar giriş yapınız." };
        var updateCustomerValidator = new UpdateCustomerDtoValidator();
        var validationResult = await updateCustomerValidator.ValidateAsync(updateCustomerDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return new ApiResponse { Success = false, Message = errors.FirstOrDefault()! };
        }

        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7001/api/Customers/Update")
            {
                Content = JsonContent.Create(updateCustomerDto),
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
            return new ApiResponse { Success = false, Message = "Müşteri güncelleme sırasında hata: " + ex.Message };
        }
    }
}