using System.Security.Claims;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Customer.Delete;

public class DeleteCustomerCommandHandler(
    ICustomerRepository repository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteCustomerCommandRequest, IResult>
{
    public async Task<IResult> Handle(DeleteCustomerCommandRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorResult("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorResult( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("manager")) 
            return new ErrorResult("Yetkisiz erişim.");

        var deleteCustomerValidator = new DeleteCustomerCommandRequestValidator();
        var validationResult = await deleteCustomerValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }

        var customer = await repository.GetAsync(request.Id);
        if (customer is null)
            return new ErrorResult("Bu ID ile müşteri bulunamadı.");
        
        if (customer is { IsDeleted: true })
            return new ErrorResult("Müşteri zaten silindi.");

        customer.IsDeleted = true;
        repository.Update(customer);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SuccessResult("Müşteri silindi.");
    }
}