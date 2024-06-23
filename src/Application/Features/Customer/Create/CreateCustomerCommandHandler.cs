using System.Security.Claims;
using AutoMapper;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Customer.Create;

public class CreateCustomerCommandHandler(
    ICustomerRepository repository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<CreateCustomerCommandRequest, IResult>
{
    public async Task<IResult> Handle(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
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

        var createCustomerValidator = new CreateCustomerCommandRequestValidator();
        var validationResult = await createCustomerValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }

        var existingCustomer = await repository.GetByNamesAsync(request.Name, request.CompanyName);
        if (existingCustomer != null)
            return new ErrorResult("Aynı müşteri adı ve firma adı ile kayıtlı bir müşteri zaten mevcut.");

        var newCustomer = mapper.Map<Domain.Entities.Customer>(request);
        await repository.InsertAsync(newCustomer);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SuccessResult("Müşteri kayıtı başarılı.");
    }
}