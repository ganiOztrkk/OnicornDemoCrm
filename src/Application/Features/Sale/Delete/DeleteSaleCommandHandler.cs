using System.Security.Claims;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Sale.Delete;

public class DeleteSaleCommandHandler(
    ISaleRepository repository,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteSaleCommandRequest, IResult>
{
    public async Task<IResult> Handle(DeleteSaleCommandRequest request, CancellationToken cancellationToken)
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

        var deleteSaleValidator = new DeleteSaleCommandRequestValidator();
        var validationResult = await deleteSaleValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }

        var sale = await repository.GetAsync(request.Id);
        if (sale is null)
            return new ErrorResult("Bu ID ile satış bulunamadı.");
        
        if (sale is { IsDeleted: true })
            return new ErrorResult("Satış zaten silindi.");

        sale.IsDeleted = true;
        repository.Update(sale);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SuccessResult("Müşteri silindi.");
    }
}