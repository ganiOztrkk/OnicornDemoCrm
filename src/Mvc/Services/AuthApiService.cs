using System.IdentityModel.Tokens.Jwt;
using Mvc.Models;
using Mvc.Validators;

namespace Mvc.Services;

public class AuthApiService(
    IHttpContextAccessor httpContextAccessor,
    IHttpClientFactory httpClientFactory)
{
    public async Task<ApiDataResponse<string>?> LoginAsync(LoginDto request)
    {
        try
        {
            // MVC tarafındaki validasyon - eğer validasyon hatası varsa server tarafına boşuna istek gitmeyecek.
            var loginValidator = new LoginDtoValidator();
            var validationResult = await loginValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage);
                return new ApiDataResponse<string> { Success = false, Message = errors.FirstOrDefault()!};
            }
            
            var client = httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("https://localhost:7001/api/Auth/Login", request);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<ApiDataResponse<string>>();
                if (responseBody!.Success)
                {
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.Now.AddHours(1)
                    };
                    httpContextAccessor.HttpContext!.Response.Cookies.Append("AccessToken", responseBody!.Data!,
                        cookieOptions);
                    
                    var tokenHandler = new JwtSecurityTokenHandler();
                    if (tokenHandler.ReadToken(responseBody.Data) is JwtSecurityToken jsonToken)
                    {
                        var userRole = jsonToken.Claims.FirstOrDefault(c => c.Type == "roles")?.Value;
                        httpContextAccessor.HttpContext.Response.Cookies.Append("Role", userRole ?? "", cookieOptions);
                    }
                    return responseBody;
                }
                else
                    return new ApiDataResponse<string> { Success = false, Message = responseBody.Message };
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Error: " + errorResponse);
                return new ApiDataResponse<string> { Success = false, Message = "API tarafından hata mesajı alındı." };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception in LoginApiService: " + ex.Message);
            return new ApiDataResponse<string>
                { Success = false, Message = "Login işlemi sırasında beklenmedik bir hata oluştu: " + ex.Message };
        }
    }

    public CookieValues GetAccessTokenAndRole()
    {
        var accessToken = httpContextAccessor.HttpContext!.Request.Cookies["AccessToken"] ?? "";
        var userRole = httpContextAccessor.HttpContext.Request.Cookies["Role"] ?? "";
        return new CookieValues() { AccessToken = accessToken, Role = userRole };
    }

    public void Logout()
    {
        try
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1)
            };
            httpContextAccessor!.HttpContext!.Response.Cookies.Append("AccessToken", "", cookieOptions);
            httpContextAccessor.HttpContext.Response.Cookies.Append("Role", "", cookieOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception in LoginApiService: " + ex.Message);
        }
    }
}