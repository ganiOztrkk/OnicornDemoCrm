using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Task.Create
{
    public class CreateTaskCommandHandler(
        ITaskRepository taskRepository,
        ITaskAttendeeRepository taskAttendeeRepository,
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor
        ) : IRequestHandler<CreateTaskCommandRequest, IResult>
    {
        public async Task<IResult> Handle(CreateTaskCommandRequest request, CancellationToken cancellationToken)
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

            var createTaskValidator = new CreateTaskCommandRequestValidator();
            var validationResult = await createTaskValidator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(errors);
            }

            var task = new Domain.Entities.Task
            {
                Title = request.Title,
                Description = request.Description,
                Deadline = request.Deadline
            };

            await taskRepository.InsertAsync(task);
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
            return new SuccessResult("Görev başarıyla oluşturuldu.");
        }
    }
}
