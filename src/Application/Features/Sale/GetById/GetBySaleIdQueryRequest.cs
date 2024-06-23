using Core.ResultPattern;
using MediatR;

namespace Application.Features.Sale.GetById;

public sealed record GetBySaleIdQueryRequest : IRequest<IDataResult<GetBySaleIdQueryResponse>>
{
    public Guid Id { get; set; }
}