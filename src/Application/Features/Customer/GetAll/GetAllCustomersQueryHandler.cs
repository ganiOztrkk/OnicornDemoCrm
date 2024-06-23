using System.Security.Claims;
using AutoMapper;
using Core.ResultPattern;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Customer.GetAll;

public class GetAllCustomersQueryHandler(
    ICustomerRepository repository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetAllCustomersQueryRequest, IDataResult<List<GetAllCustomersQueryResponse>>>
{
    public async Task<IDataResult<List<GetAllCustomersQueryResponse>>> Handle(GetAllCustomersQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorDataResult<List<GetAllCustomersQueryResponse>>("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorDataResult<List<GetAllCustomersQueryResponse>>( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("manager") && !roles.Contains("salesperson")) 
            return new ErrorDataResult<List<GetAllCustomersQueryResponse>>("Yetkisiz erişim.");
        
        var customers = await repository.GetAllAsync();
        if (!customers.Any()) 
            return new ErrorDataResult<List<GetAllCustomersQueryResponse>>("Müşteri bulunamadı.");
        var activeCustomers = customers.Where(x => x is { IsDeleted: false }).ToList();
        
        var response = mapper.Map<List<GetAllCustomersQueryResponse>>(activeCustomers);
        return new SuccessDataResult<List<GetAllCustomersQueryResponse>>(response);
    }
}