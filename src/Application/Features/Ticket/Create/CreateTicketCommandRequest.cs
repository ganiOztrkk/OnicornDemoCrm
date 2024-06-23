using Core.ResultPattern;
using MediatR;

namespace Application.Features.Ticket.Create;

public sealed class CreateTicketCommandRequest : IRequest<IResult>
{
    public string Subject { get; set; } = string.Empty; 
    public string Summary { get; set; } = string.Empty;
} 