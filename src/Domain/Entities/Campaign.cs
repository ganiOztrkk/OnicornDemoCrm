using Core.GenericRepository;

namespace Domain.Entities;

public sealed class Campaign : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Sector { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}