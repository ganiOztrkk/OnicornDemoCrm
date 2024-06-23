using System.Security.Claims;
using Application.Features.Customer.Create;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Announcement.Create;

public class CreateAnnouncementCommandHandler(
    IAnnouncementRepository repository,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<CreateAnnouncementCommandRequest, IResult>
{
    public async Task<IResult> Handle(CreateAnnouncementCommandRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorResult("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorResult( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("coordinator") && !roles.Contains("manager")) 
            return new ErrorResult("Yetkisiz erişim.");

        var createAnnouncementValidator = new CreateAnnouncementCommandRequestValidator();
        var validationResult = await createAnnouncementValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }

        var announcement = new Domain.Entities.Announcement
        {
            Title = request.Title,
            Content = request.Content,
            CreateDate = DateTime.Now,
            IsDeleted = false
        };
        
        await repository.InsertAsync(announcement);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SuccessResult("Duyuru yayınlandı.");
    }
}