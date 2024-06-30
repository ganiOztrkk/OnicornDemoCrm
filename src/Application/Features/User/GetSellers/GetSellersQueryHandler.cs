using System.Security.Claims;
using Core.ResultPattern;
using Domain.Entities;
using Infastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User.GetSellers;

public class GetSellersQueryHandler(
    UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor,
    ApplicationDbContext context,
    RoleManager<AppRole> roleManager
    ) : IRequestHandler<GetSellersQueryRequest, IDataResult<List<AppUser>>>
{
    public async Task<IDataResult<List<AppUser>>> Handle(GetSellersQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorDataResult<List<AppUser>>("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorDataResult<List<AppUser>>( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && roles.Contains("coordinator") && roles.Contains("manager") && roles.Contains("salesperson")) 
            return new ErrorDataResult<List<AppUser>>("Yetkisiz erişim.");

        var users = await userManager.Users.ToListAsync(cancellationToken);
        if (users is null)
            return new ErrorDataResult<List<AppUser>>("Kullanıcı bulunamadı.");
        
        // burada işlemler
        var salespersonRole = await roleManager.Roles.FirstOrDefaultAsync(r => r.Name == "salesperson", cancellationToken);
        if (salespersonRole is null)
            return new ErrorDataResult<List<AppUser>>("Satış elemanı rolü bulunamadı.");
        var salespersonRoleId = salespersonRole.Id;

        // 'salesperson' rolüne sahip kullanıcı ID'lerini al
        var salespersonUserIds = await context.UserRoles
            .Where(ur => ur.RoleId == salespersonRoleId)
            .Select(ur => ur.UserId)
            .ToListAsync(cancellationToken);

        if (!salespersonUserIds.Any())
            return new ErrorDataResult<List<AppUser>>("Satış elemanı bulunamadı.");
        
        var salespersonUsers = await userManager.Users
            .Where(u => salespersonUserIds.Contains(u.Id))
            .ToListAsync(cancellationToken);

        if (!salespersonUsers.Any())
            return new ErrorDataResult<List<AppUser>>("Satış elemanı bulunamadı.");
        
        return new SuccessDataResult<List<AppUser>>(salespersonUsers);
    }
}