using Core.ResultPattern;
using MediatR;

namespace Application.Features.Meeting.GetByUserId;

public class GetMeetingByUserIdQueryRequest : IRequest<IDataResult<List<GetMeetingByUserIdQueryResponse>>>
{
    public Guid UserId { get; set; }
}