using System.Security.Claims;
using Application.Features.Customer.Delete;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Meeting.Delete;

public class DeleteMeetingCommandHandler(
    IMeetingRepository repository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteMeetingCommandRequest, IResult>
{
    public async Task<IResult> Handle(DeleteMeetingCommandRequest request, CancellationToken cancellationToken)
    {
        
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorResult("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorResult( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("manager") && !roles.Contains("coordinator")) 
            return new ErrorResult("Yetkisiz erişim.");

        var deleteMeetingValidator = new DeleteMeetingCommandRequestValidator();
        var validationResult = await deleteMeetingValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }

        var meeting = await repository.GetAsync(request.MeetingId);
        if (meeting is null)
            return new ErrorResult("Bu ID ile toplantı bulunamadı.");
        
        if (meeting is { IsDeleted: true })
            return new ErrorResult("Toplantı zaten silindi.");

        meeting.IsDeleted = true;
        repository.Update(meeting);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SuccessResult("Toplantı silindi.");
    }
}