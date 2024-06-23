using System.Security.Claims;
using AutoMapper;
using Core.ResultPattern;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Customer.GetById;

public class GetByCustomerIdQueryHandler(
    ICustomerRepository repository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetByCustomerIdQueryRequest, IDataResult<GetByCustomerIdQueryResponse>>
{
    public async Task<IDataResult<GetByCustomerIdQueryResponse>> Handle(GetByCustomerIdQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorDataResult<GetByCustomerIdQueryResponse>("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorDataResult<GetByCustomerIdQueryResponse>( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("coordinator") && !roles.Contains("manager")) 
            return new ErrorDataResult<GetByCustomerIdQueryResponse>("Yetkisiz erişim.");
        
        var getByCustomerIdValidator = new GetByCustomerIdQueryRequestValidator();
        var validationResult = await getByCustomerIdValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorDataResult<GetByCustomerIdQueryResponse>(errors);
        }
        
        var customer = await repository.GetAsync(request.Id);
        if (customer is null) 
            return new ErrorDataResult<GetByCustomerIdQueryResponse>("Müşteri bulunamadı.");
        
        var response = mapper.Map<GetByCustomerIdQueryResponse>(customer);
        return new SuccessDataResult<GetByCustomerIdQueryResponse>(response);
    }
}