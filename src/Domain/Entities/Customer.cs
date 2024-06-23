using Core.GenericRepository;

namespace Domain.Entities;

public sealed class Customer : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? CompanyName { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string? Sector { get; set; }
    public string? ShoppingArea { get; set; }  
    public bool IsDeleted { get; set; } = false;
    
    public ICollection<Sale>? Sales { get; set; }
}