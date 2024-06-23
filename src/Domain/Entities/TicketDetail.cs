using Core.GenericRepository;

namespace Domain.Entities;

public sealed class TicketDetail : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TicketId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}