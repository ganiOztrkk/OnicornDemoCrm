using System.Security.Claims;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Meeting.Create;

public class CreateMeetingCommandHandler(
    IMeetingRepository repository,
    IMeetingAttendeeRepository meetingAttendeeRepository,
    IHttpContextAccessor httpContextAccessor,
    UserManager<AppUser> userManager,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateMeetingCommandRequest, IResult>
{
    public async Task<IResult> Handle(CreateMeetingCommandRequest request, CancellationToken cancellationToken)
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

        var createMeetingValidator = new CreateMeetingCommandRequestValidator();
        var validationResult = await createMeetingValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }

        var meeting = new Domain.Entities.Meeting
        {
            Title = request.Title,
            Description = request.Description,
            StartDate = request.StartDate,
            Duration = request.Duration
        };
        await repository.InsertAsync(meeting);
        foreach (var id in request.UserIds)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user is null) 
                return new ErrorResult("Kullanıcı atama hatası. Bu ID ile kullanıcı yok.");
            var meetingAttendee = new MeetingAttendee
            {
                MeetingId = meeting.Id,
                UserId = id
            };
            await meetingAttendeeRepository.InsertAsync(meetingAttendee);
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SuccessResult("Toplantı başarıyla oluşturuldu.");
    }
}