using Core.ResultPattern;
using MediatR;

namespace Application.Features.Ticket.CreateDetailContent;

public sealed class CreateTicketDetailContentCommandRequest : IRequest<IResult>
{
    public Guid TicketId { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid AppUserId { get; set; }
}