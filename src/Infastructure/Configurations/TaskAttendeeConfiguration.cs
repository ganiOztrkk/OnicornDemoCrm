﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Configurations
{
    public class TaskAttendeeConfiguration : IEntityTypeConfiguration<TaskAttendee>
    {
        public void Configure(EntityTypeBuilder<TaskAttendee> builder)
        {
            builder.ToTable("TaskAttendees");
            builder.HasKey(x => new { x.TaskId, x.UserId });

            builder.HasOne(x => x.Task)
                .WithMany(x => x.Attendees)
                .HasForeignKey(x => x.TaskId);

            builder.HasOne(ma => ma.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(ma => ma.UserId);


        }
    }
}