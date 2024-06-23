using System.ComponentModel.DataAnnotations;
using Core.ResultPattern;
using MediatR;

namespace Application.Features.Customer.GetById;

public sealed record GetByCustomerIdQueryRequest : IRequest<IDataResult<GetByCustomerIdQueryResponse>>
{
    public Guid Id { get; set; }
}