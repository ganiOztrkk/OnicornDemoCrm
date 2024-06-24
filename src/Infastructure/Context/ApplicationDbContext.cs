using System.Reflection;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Entities.Task;

namespace Infastructure.Context;

public sealed class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
{
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppUserRole> AppUserRoles  { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingAttendee> MeetingAttendees { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketDetail> TicketDetails { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<TaskAttendee> TaskAttendees { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}