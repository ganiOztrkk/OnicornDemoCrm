using System.Security.Claims;
using Core.ResultPattern;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User.GetAll;

public class GetAllUsersQueryHandler(
    UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetAllUsersQueryRequest, IDataResult<List<AppUser>>>
{
    public async Task<IDataResult<List<AppUser>>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorDataResult<List<AppUser>>("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorDataResult<List<AppUser>>( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin")) 
            return new ErrorDataResult<List<AppUser>>("Yetkisiz erişim.");

        var users = await userManager.Users.ToListAsync(cancellationToken);
        if (users is null)
            return new ErrorDataResult<List<AppUser>>("Kullanıcı bulunamadı.");
        return new SuccessDataResult<List<AppUser>>(users);
    }
}