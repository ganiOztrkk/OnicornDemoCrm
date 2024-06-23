using Core.GenericRepository;

namespace Domain.Entities;

public sealed class Ticket : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public string Subject { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public bool IsDeleted { get; set; } = false;
    
}