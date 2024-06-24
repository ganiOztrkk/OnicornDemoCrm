using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Task.Delete
{
    public class DeleteTaskCommandHandler(
    ITaskRepository repository,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<DeleteTaskCommandRequest, IResult>
    {
        public async Task<IResult> Handle(DeleteTaskCommandRequest request, CancellationToken cancellationToken)
        {

            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext is null)
                return new ErrorResult("Access Token bulunamadı.");

            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (userId is null)
                return new ErrorResult("Kullanıcı girişi yapın.");

            var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (!roles.Contains("admin") && !roles.Contains("manager") && !roles.Contains("coordinator"))
                return new ErrorResult("Yetkisiz erişim.");

            var deleteTaskValidator = new DeleteTaskCommandRequestValidator();
            var validationResult = await deleteTaskValidator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(errors);
            }

            var task = await repository.GetAsync(request.TaskId);
            if (task is null)
                return new ErrorResult("Bu ID ile görev bulunamadı.");

            if (task is { IsDeleted: true })
                return new ErrorResult("Görev zaten silindi.");

            task.IsDeleted = true;
            repository.Update(task);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new SuccessResult("Görev silindi.");
        }
    }
}
