using Core.GenericRepository;

namespace Domain.Entities;

public sealed class Sale : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public bool IsPaid { get; set; }
    public bool IsDeleted { get; set; } = false;

    public Guid SalesPersonId { get; set; }
    public AppUser? SalesPerson { get; set; }

    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
}