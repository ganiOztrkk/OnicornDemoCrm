using System.Security.Claims;
using AutoMapper;
using Core.ResultPattern;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Sale.GetAll;

public class GetAllSaleQueryHandler(
    ISaleRepository repository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<GetAllSaleQueryRequest, IDataResult<List<GetAllSaleQueryResponse>>>
{
    public async Task<IDataResult<List<GetAllSaleQueryResponse>>> Handle(GetAllSaleQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorDataResult<List<GetAllSaleQueryResponse>>("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorDataResult<List<GetAllSaleQueryResponse>>( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("manager") && !roles.Contains("salesperson")) 
            return new ErrorDataResult<List<GetAllSaleQueryResponse>>("Yetkisiz erişim.");
        
        var sales = await repository.GetAllAsync();
        if (!sales.Any()) 
            return new ErrorDataResult<List<GetAllSaleQueryResponse>>("Satış bulunamadı.");
        var activeSales = sales.Where(x => x is { IsDeleted: false }).ToList();
        
        var response = mapper.Map<List<GetAllSaleQueryResponse>>(activeSales);
        return new SuccessDataResult<List<GetAllSaleQueryResponse>>(response);
    }
}