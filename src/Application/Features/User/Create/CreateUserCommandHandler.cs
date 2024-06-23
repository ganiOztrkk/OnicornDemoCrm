using System.Security.Claims;
using AutoMapper;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Entities;
using Infastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.User.Create;

public class CreateUserCommandHandler(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ApplicationDbContext dbContext
    ) : IRequestHandler<CreateUserCommandRequest, IResult>
{
    public async Task<IResult> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
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

        var createUserValidator = new CreateUserCommandRequestValidator();
        var validationResult = await createUserValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }
        
        var existingUserByEmail = await userManager.FindByEmailAsync(request.Email);
        if (existingUserByEmail != null)
            return new ErrorResult("Bu e-posta adresi zaten kullanılıyor.");
        

        var existingUserByUsername = await userManager.FindByNameAsync(request.Email);
        if (existingUserByUsername != null)
            return new ErrorResult("Bu kullanıcı adı zaten kullanılıyor.");

        var appUser = mapper.Map<AppUser>(request);
        appUser.EmailConfirmed = true;
        appUser.UserName = appUser.Email;
        var role = await roleManager.FindByNameAsync(request.RoleName);
        if (role is null)
            return new ErrorResult("İlgili rol bulunamadı.");
        
        var createUserResult = await userManager.CreateAsync(appUser, request.Password);
        if (!createUserResult.Succeeded)
        {
            var errors = string.Join(",", createUserResult.Errors.Select(e => e.Description));
            return new ErrorResult(errors);
        }
        var appUserRole = new AppUserRole
        {
            UserId = appUser.Id,
            RoleId = role.Id
        };
        
        await dbContext.AppUserRoles.AddAsync(appUserRole, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SuccessResult("Kullanıcı başarıyla oluşturuldu.");
    }
}