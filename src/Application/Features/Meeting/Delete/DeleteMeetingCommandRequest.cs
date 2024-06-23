using Core.ResultPattern;
using MediatR;

namespace Application.Features.Meeting.Delete;

public class DeleteMeetingCommandRequest : IRequest<IResult>
{
    public Guid MeetingId { get; set; }
}