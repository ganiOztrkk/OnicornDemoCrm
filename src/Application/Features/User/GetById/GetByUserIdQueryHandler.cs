using System.Security.Claims;
using Core.ResultPattern;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User.GetById;

public class GetByUserIdQueryHandler(
    UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetByUserIdQueryRequest, IDataResult<AppUser>>
{
    public async Task<IDataResult<AppUser>> Handle(GetByUserIdQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorDataResult<AppUser>("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorDataResult<AppUser>( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin")) 
            return new ErrorDataResult<AppUser>("Yetkisiz erişim.");
        
        var getByUserIdValidator = new GetByUserIdQueryRequestValidator();
        var validationResult = await getByUserIdValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorDataResult<AppUser>(errors);
        }

        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            return new ErrorDataResult<AppUser>("Kullanıcı bulunamadı.");
        return new SuccessDataResult<AppUser>(user);
    }
}