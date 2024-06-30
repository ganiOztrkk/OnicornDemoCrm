using System.Security.Claims;
using Application.Features.Customer.Update;
using AutoMapper;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Entities;
using Infastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.User.Update;

public class UpdateUserCommandHandler(
    UserManager<AppUser> userManager,
    IUnitOfWork unitOfWork,
    RoleManager<AppRole> roleManager,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper,
    ApplicationDbContext dbContext
    ) : IRequestHandler<UpdateUserCommandRequest, IResult>
{
    public async Task<IResult> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorResult("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorResult( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin")) 
            return new ErrorResult("Yetkisiz erişim.");

        var updateUserValidator = new UpdateUserCommandRequestValidator();
        var validationResult = await updateUserValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }

        var existingUser = await userManager.FindByIdAsync(request.Id.ToString());
        if (existingUser is null)
            return new ErrorResult("Bu ID ile kullanıcı bulunamadı.");
        
        if (existingUser.Email != request.Email)
        {
            var duplicateUser = await userManager.FindByEmailAsync(request.Email);
            if (duplicateUser is not null)
                return new ErrorResult("Aynı mail ile kayıtlı bir kullanıcı zaten mevcut.");
        }
        mapper.Map(request, existingUser);

        var userRoles = await roleManager.Roles.Select(x => x.Name).ToListAsync(cancellationToken);
        if (!userRoles.Contains(request.RoleName))
            return new ErrorResult("Geçerli bir rol giriniz.");
        
        var role = await roleManager.FindByNameAsync(request.RoleName);
        
        var existingUserRoles = await userManager.GetRolesAsync(existingUser);
        var removeRolesResult = await userManager.RemoveFromRolesAsync(existingUser, existingUserRoles);
        if (!removeRolesResult.Succeeded)
            return new ErrorResult("Mevcut rol güncellenirken hata oluştu.");
        
        var appUserRole = new AppUserRole
        {
            UserId = existingUser.Id,
            RoleId = role!.Id
        };
        await dbContext.AppUserRoles.AddAsync(appUserRole, cancellationToken);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SuccessResult("Güncelleme başarılı.");
    }
}