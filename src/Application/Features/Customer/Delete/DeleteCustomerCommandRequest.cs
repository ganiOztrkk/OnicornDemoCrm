using Core.ResultPattern;
using MediatR;

namespace Application.Features.Customer.Delete;

public sealed record DeleteCustomerCommandRequest : IRequest<IResult>
{
    public Guid Id { get; set; }
}