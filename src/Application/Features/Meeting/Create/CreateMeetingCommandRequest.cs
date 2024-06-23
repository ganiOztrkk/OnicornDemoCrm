using Core.ResultPattern;
using MediatR;

namespace Application.Features.Meeting.Create;

public class CreateMeetingCommandRequest : IRequest<IResult>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }
    public List<Guid> UserIds { get; set; } = new List<Guid>();
}