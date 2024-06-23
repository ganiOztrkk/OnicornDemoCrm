using System.Security.Claims;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Announcement.Delete;

public class DeleteAnnouncementCommandHandler(
    IAnnouncementRepository repository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteAnnouncementCommandRequest, IResult>
{
    public async Task<IResult> Handle(DeleteAnnouncementCommandRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorResult("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorResult( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("manager")) 
            return new ErrorResult("Yetkisiz erişim.");

        var deleteAnnouncementValidator = new DeleteAnnouncementCommandRequestValidator();
        var validationResult = await deleteAnnouncementValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }

        var announcement = await repository.GetAsync(request.AnnouncementId);
        if (announcement is null)
            return new ErrorResult("Duyuru bulunamadı.");
        
        repository.Delete(announcement);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SuccessResult("Duyuru silindi.");
    }
}