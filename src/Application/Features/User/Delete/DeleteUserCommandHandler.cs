using System.Security.Claims;
using Application.Features.Customer.Delete;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.User.Delete;

public class DeleteUserCommandHandler(
    UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteUserCommandRequest, IResult>
{
    public async Task<IResult> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
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

        var deleteUserValidator = new DeleteUserCommandRequestValidator();
        var validationResult = await deleteUserValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }


        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            return new ErrorResult("Kullanıcı bulunamadı.");
        user.IsDeleted = true;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SuccessResult("Kullanıcı silindi.");
    }
}