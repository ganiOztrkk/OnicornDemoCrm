using Core.GenericRepository;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUserRole : IdentityUserRole<Guid>, IEntity
{
}