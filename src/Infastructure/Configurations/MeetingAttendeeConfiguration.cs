using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infastructure.Configurations;

public class MeetingAttendeeConfiguration : IEntityTypeConfiguration<MeetingAttendee>
{
    public void Configure(EntityTypeBuilder<MeetingAttendee> builder)
    {
        builder.ToTable("MeetingAttendees");
        builder.HasKey(x => new { x.MeetingId, x.UserId });

        builder.HasOne(x => x.Meeting)
            .WithMany(x => x.Attendees)
            .HasForeignKey(x => x.MeetingId);
        
        builder.HasOne(ma => ma.User)
            .WithMany(u => u.Meetings)
            .HasForeignKey(ma => ma.UserId);
    }
}