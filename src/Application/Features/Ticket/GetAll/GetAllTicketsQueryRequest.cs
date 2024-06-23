using Core.ResultPattern;
using MediatR;

namespace Application.Features.Ticket.GetAll;

public sealed class GetAllTicketsQueryRequest : IRequest<IDataResult<List<GetAllTicketsQueryResponse>>>
{ }