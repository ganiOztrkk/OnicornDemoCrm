using Core.ResultPattern;
using MediatR;

namespace Application.Features.Sale.Delete;

public sealed record DeleteSaleCommandRequest : IRequest<IResult>
{
    public Guid Id { get; set; }
}