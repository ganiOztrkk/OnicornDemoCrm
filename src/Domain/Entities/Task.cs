using Core.GenericRepository;

namespace Domain.Entities;

public sealed class Task : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsDeleted { get; set; } = false;
}