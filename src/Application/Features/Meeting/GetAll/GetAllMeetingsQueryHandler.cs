using System.Security.Claims;
using Core.ResultPattern;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Meeting.GetAll;

public class GetAllMeetingsQueryHandler(
    IMeetingRepository meetingRepository,
    IMeetingAttendeeRepository meetingAttendeeRepository,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetAllMeetingsQueryRequest, IDataResult<List<GetAllMeetingsQueryResponse>>>
{
    public async Task<IDataResult<List<GetAllMeetingsQueryResponse>>> Handle(GetAllMeetingsQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorDataResult<List<GetAllMeetingsQueryResponse>>("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorDataResult<List<GetAllMeetingsQueryResponse>>( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("coordinator") && !roles.Contains("manager")) 
            return new ErrorDataResult<List<GetAllMeetingsQueryResponse>>("Yetkisiz erişim.");
        
        var meetings = await meetingRepository.GetAllAsync();
        meetings = meetings.Where(x => x!.IsDeleted == false).ToList();
        if (!meetings.Any())
            return new ErrorDataResult<List<GetAllMeetingsQueryResponse>>("Toplantı bulunamadı.");
        
        var meetingResponse = new List<GetAllMeetingsQueryResponse>();
        foreach (var meeting in meetings)
        {
            var attendees = await meetingAttendeeRepository.GetAllAsync();
            var userIds = attendees
                .Where(a => a!.MeetingId == meeting!.Id)
                .Select(a => a!.UserId)
                .ToList();

            var response = new GetAllMeetingsQueryResponse
            {
                Id = meeting!.Id,
                Title = meeting!.Title,
                Description = meeting.Description,
                StartDate = meeting.StartDate,
                Duration = meeting.Duration,
                UserIds = userIds
            };
            meetingResponse.Add(response);
        }

        return new SuccessDataResult<List<GetAllMeetingsQueryResponse>>(meetingResponse);
    }
}