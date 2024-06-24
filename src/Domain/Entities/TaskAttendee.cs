using Core.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class TaskAttendee : IEntity
    {
        public Guid TaskId { get; set; }
        public Task? Task { get; set; }
        public Guid UserId { get; set; }
        public AppUser? User { get; set; }
    }
}
