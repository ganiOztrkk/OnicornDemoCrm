using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infastructure.Configurations;

public class TicketDetailConfiguration : IEntityTypeConfiguration<TicketDetail>
{
    public void Configure(EntityTypeBuilder<TicketDetail> builder)
    {
        builder.ToTable("TicketDetails");
        builder.HasOne(x => x.AppUser)
            .WithMany()
            .HasForeignKey(x => x.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}