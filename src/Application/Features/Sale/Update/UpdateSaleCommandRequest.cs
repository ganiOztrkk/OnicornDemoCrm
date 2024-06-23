using Core.ResultPattern;
using MediatR;

namespace Application.Features.Sale.Update;

public sealed record UpdateSaleCommandRequest : IRequest<IResult>
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public bool IsPaid { get; set; }

    public Guid SalesPersonId { get; set; }
    public Guid CustomerId { get; set; }
}