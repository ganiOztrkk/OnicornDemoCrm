using Core.ResultPattern;
using MediatR;

namespace Application.Features.Sale.Create;

public sealed record CreateSaleCommandRequest : IRequest<IResult>
{
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public bool IsPaid { get; set; }

    public Guid SalesPersonId { get; set; }
    public Guid CustomerId { get; set; }
}