using System.Security.Claims;
using Core.ResultPattern;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Meeting.GetByUserId;

public class GetMeetingByUserIdQueryHandler(
    IMeetingRepository repository,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetMeetingByUserIdQueryRequest, IDataResult<List<GetMeetingByUserIdQueryResponse>>>
{
    public async Task<IDataResult<List<GetMeetingByUserIdQueryResponse>>> Handle(GetMeetingByUserIdQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorDataResult<List<GetMeetingByUserIdQueryResponse>>("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorDataResult<List<GetMeetingByUserIdQueryResponse>>( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("coordinator") && !roles.Contains("manager") && !roles.Contains("salesperson")) 
            return new ErrorDataResult<List<GetMeetingByUserIdQueryResponse>>("Yetkisiz erişim.");

        var meetings = await repository.GetMeetingsByUserIdAsync(request.UserId);
        if (!meetings.Any())
            return new ErrorDataResult<List<GetMeetingByUserIdQueryResponse>>("Toplantı bulunamadı.");
        var meetingList = new List<GetMeetingByUserIdQueryResponse>();
        foreach (var item in meetings)
        {
            var meeting = new GetMeetingByUserIdQueryResponse
            {
                Title = item.Title,
                Description = item.Description,
                StartDate = item.StartDate,
                Duration = item.Duration
            };
            meetingList.Add(meeting);
        }
        return new SuccessDataResult<List<GetMeetingByUserIdQueryResponse>>(meetingList);
    }
}