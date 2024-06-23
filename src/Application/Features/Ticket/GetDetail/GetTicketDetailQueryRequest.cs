using Core.ResultPattern;
using Domain.Entities;
using MediatR;

namespace Application.Features.Ticket.GetDetail;

public sealed class GetTicketDetailQueryRequest : IRequest<IDataResult<List<TicketDetail>>>
{
    public Guid TicketId { get; set; }
}