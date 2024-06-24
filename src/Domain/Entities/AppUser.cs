using Core.GenericRepository;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public sealed class AppUser : IdentityUser<Guid>, IEntity
{
    public string Name { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public  bool IsDeleted { get; set; } = false;
    
    public ICollection<MeetingAttendee>? Meetings { get; set; }
    public ICollection<TaskAttendee>? Tasks { get; set; }
}