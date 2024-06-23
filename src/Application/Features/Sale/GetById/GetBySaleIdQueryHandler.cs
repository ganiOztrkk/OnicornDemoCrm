using System.Security.Claims;
using Application.Features.Customer.GetById;
using AutoMapper;
using Core.ResultPattern;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Sale.GetById;

public class GetBySaleIdQueryHandler(
    ISaleRepository repository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<GetBySaleIdQueryRequest, IDataResult<GetBySaleIdQueryResponse>>
{
    public async Task<IDataResult<GetBySaleIdQueryResponse>> Handle(GetBySaleIdQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorDataResult<GetBySaleIdQueryResponse>("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorDataResult<GetBySaleIdQueryResponse>( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("salesperson") && !roles.Contains("manager")) 
            return new ErrorDataResult<GetBySaleIdQueryResponse>("Yetkisiz erişim.");
        
        var getBySaleIdValidator = new GetBySaleIdQueryRequestValidator();
        var validationResult = await getBySaleIdValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            return new ErrorDataResult<GetBySaleIdQueryResponse>(errors);
        }
        
        var sale = await repository.GetAsync(request.Id);
        if (sale is null) 
            return new ErrorDataResult<GetBySaleIdQueryResponse>("Satış bulunamadı.");
        
        var response = mapper.Map<GetBySaleIdQueryResponse>(sale);
        return new SuccessDataResult<GetBySaleIdQueryResponse>(response);
    }
}