using System.Security.Claims;
using AutoMapper;
using Core.ResultPattern;
using Core.UnitOfWork;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using IResult = Core.ResultPattern.IResult;

namespace Application.Features.Sale.Create;

public class CreateSaleCommandHandler(
    ISaleRepository repository,
    ICustomerRepository customerRepository,
    UserManager<AppUser> userManager,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<CreateSaleCommandRequest, IResult>
{
    public async Task<IResult> Handle(CreateSaleCommandRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorResult("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorResult( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("salesperson") && !roles.Contains("manager")) 
            return new ErrorResult("Yetkisiz erişim.");

        var createSaleValidator = new CreateSaleCommandRequestValidator();
        var validationResult = await createSaleValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorResult(errors);
        }
        
        var customerExists = await customerRepository.ExistsAsync(request.CustomerId);
        if (!customerExists)
            return new ErrorResult("Geçersiz Müşteri ID'si.");
        
        var salesPersonExists = await userManager.FindByIdAsync(request.SalesPersonId.ToString()) != null;
        if (!salesPersonExists)
            return new ErrorResult("Geçersiz Satış Personeli ID'si.");
        

        var newSale = mapper.Map<Domain.Entities.Sale>(request);
        await repository.InsertAsync(newSale);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new SuccessResult("Satış kayıtı başarılı.");
    }
}