namespace Application.Features.Sale.GetAll;

public sealed record GetAllSaleQueryResponse
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public bool IsPaid { get; set; }

    public Guid SalesPersonId { get; set; }
    public Guid CustomerId { get; set; }
}