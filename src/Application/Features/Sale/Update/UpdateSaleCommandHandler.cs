using System.Security.Claims;
using AutoMapper;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Sale.Update;

public class UpdateSaleCommandHandler(
    ISaleRepository repository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateSaleCommandRequest, IResult>
{
    public async Task<IResult> Handle(UpdateSaleCommandRequest request, CancellationToken cancellationToken)
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

        var updateSaleValidator = new UpdateSaleCommandRequestValidator();
        var validationResult = await updateSaleValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }

        var existingSale = await repository.GetAsync(request.Id);
        if (existingSale is null)
            return new ErrorResult("Bu ID ile satış bulunamadı.");
        
        mapper.Map(request, existingSale); // Mevcut entity'yi güncelle
        
        repository.Update(existingSale); // Güncellenmiş entity'yi izlemeye al
        await unitOfWork.SaveChangesAsync(cancellationToken); // Değişiklikleri kaydet

        return new SuccessResult("Güncelleme başarılı.");
    }
}
