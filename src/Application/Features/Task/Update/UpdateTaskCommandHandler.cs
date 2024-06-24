using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Task.Update
{
    public class UpdateTaskCommandHandler(
    ITaskRepository taskRepository,
    ITaskAttendeeRepository taskAttendeeRepository,
    IHttpContextAccessor httpContextAccessor,
    UserManager<AppUser> userManager,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateTaskCommandRequest, IResult>
    {
        public async Task<IResult> Handle(UpdateTaskCommandRequest request, CancellationToken cancellationToken)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext is null)
                return new ErrorResult("Access Token bulunamadı.");

            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (userId is null)
                return new ErrorResult("Kullanıcı girişi yapın.");

            var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (!roles.Contains("admin") && !roles.Contains("coordinator") && !roles.Contains("manager"))
                return new ErrorResult("Yetkisiz erişim.");

            var updateTaskValidator = new UpdateTaskCommandRequestValidator();
            var validationResult = await updateTaskValidator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(errors);
            }

            var task = await taskRepository.GetAsync(request.Id);
            if (task is null)
                return new ErrorResult("Görev bulunamadı.");

            task.Title = request.Title;
            task.Description = request.Description;
            task.Deadline = request.Deadline;

            taskRepository.Update(task);

            var existingAttendees = await taskAttendeeRepository.GetByTaskIdAsync(task.Id);
            foreach (var attendee in existingAttendees)
            {
                taskAttendeeRepository.Delete(attendee);
            }

            foreach (var id in request.UserIds)
            {
                var user = await userManager.FindByIdAsync(id.ToString());
                if (user is null)
                    return new ErrorResult("Kullanıcı atama hatası. Bu ID ile kullanıcı yok.");
                var taskAttendee = new TaskAttendee
                {
                    TaskId = task.Id,
                    UserId = id
                };
                await taskAttendeeRepository.InsertAsync(taskAttendee);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new SuccessResult("Görev başarıyla güncellendi.");
        }
    }
}
