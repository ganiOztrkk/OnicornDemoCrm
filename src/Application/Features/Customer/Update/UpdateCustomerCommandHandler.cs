using System.Security.Claims;
using Application.Features.Customer.Create;
using AutoMapper;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Customer.Update;

public class UpdateCustomerCommandHandler(
    ICustomerRepository repository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateCustomerCommandRequest, IResult>
{
    public async Task<IResult> Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorResult("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorResult( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("coordinator") && !roles.Contains("manager")) 
            return new ErrorResult("Yetkisiz erişim.");

        var updateCustomerValidator = new UpdateCustomerCommandRequestValidator();
        var validationResult = await updateCustomerValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }

        var existingCustomer = await repository.GetAsync(request.Id);
        if (existingCustomer is null)
            return new ErrorResult("Bu ID ile müşteri bulunamadı.");
        
        var duplicateCustomer = await repository.GetByNamesAsync(request.Name, request.CompanyName);
        if (duplicateCustomer != null && duplicateCustomer.Id != request.Id)
            return new ErrorResult("Aynı müşteri adı ve firma adı ile kayıtlı bir müşteri zaten mevcut.");
        
        mapper.Map(request, existingCustomer);

        repository.Update(existingCustomer);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new SuccessResult("Güncelleme başarılı.");
    }
}
