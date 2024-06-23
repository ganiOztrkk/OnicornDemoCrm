using Core.ResultPattern;
using MediatR;

namespace Application.Features.Ticket.GetById;

public sealed class GetByTicketIdQueryRequest : IRequest<IDataResult<Domain.Entities.Ticket>>
{
    public Guid TicketId { get; set; }
}