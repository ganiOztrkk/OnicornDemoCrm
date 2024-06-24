using Core.GenericRepository;
using Domain.Entities;
using Domain.Repositories;
using Infastructure.Context;

namespace Infastructure.Repositories
{
    public class TaskAttendeeRepository(ApplicationDbContext context) : GenericRepository<TaskAttendee, ApplicationDbContext>(context), ITaskAttendeeRepository
    {
    }
}
