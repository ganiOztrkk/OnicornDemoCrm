using Core.GenericRepository;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public sealed class AppRole : IdentityRole<Guid>, IEntity
{
    public bool IsDeleted { get; set; } = false;
}