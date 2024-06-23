using Core.GenericRepository;

namespace Domain.Entities;

public sealed class Announcement : IEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public bool IsDeleted { get; set; }
}