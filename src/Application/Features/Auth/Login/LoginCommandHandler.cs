using Core.ResultPattern;
using Domain.Entities;
using Domain.Services;
using Infastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Login;

internal sealed class LoginCommandHandler(
    UserManager<AppUser> userManager,
    ApplicationDbContext context,
    IJwtProvider jwtProvider) : IRequestHandler<LoginCommandRequest, IDataResult<string>>
{
    public async Task<IDataResult<string>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        var loginValidator = new LoginCommandValidator();
        var validationResult = await loginValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorDataResult<string>(errors);
        }
        
        var appUser = await userManager.FindByNameAsync(request.EmailOrUserName);
        if (appUser is null)
        {
            appUser = await userManager.FindByEmailAsync(request.EmailOrUserName);
            if (appUser is null) return new ErrorDataResult<string>("Kullanıcı adı yada Mail hatalı");
        }

        if (appUser.IsDeleted)
            return new ErrorDataResult<string>("Kullanıcı silindi.");

        var userRoles = await context.UserRoles
            .Where(x => x.UserId == appUser.Id)
            .Select(x => x.RoleId)
            .ToListAsync(cancellationToken);
        
        var roles = new List<string>();
        foreach (var item in userRoles)
        {
            var roleName = context.Roles
                .Where(x => x.Id == item)
                .Select(x => x.Name)
                .FirstOrDefault();
            if (roleName is not null)
            {
                roles.Add(roleName);
            }
        }
        
        var checkPassword = await userManager.CheckPasswordAsync(appUser, request.Password);
        if (!checkPassword)
        {
            return new ErrorDataResult<string>("Şifre Hatalı");
            
        }
        var token = await jwtProvider.CreateTokenAsync(appUser, roles);
        return new SuccessDataResult<string>(token.Data!, "Giriş başarılı.");
    }
}