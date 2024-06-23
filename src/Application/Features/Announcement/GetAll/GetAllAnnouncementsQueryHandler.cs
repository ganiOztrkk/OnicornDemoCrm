using System.Security.Claims;
using AutoMapper;
using Core.ResultPattern;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Announcement.GetAll;

public class GetAllAnnouncementsQueryHandler(
    IHttpContextAccessor httpContextAccessor,
    IAnnouncementRepository repository,
    IMapper mapper
    ) : IRequestHandler<GetAllAnnouncementsQueryRequest, IDataResult<List<GetAllAnnouncementsQueryResponse>>>
{
    public async Task<IDataResult<List<GetAllAnnouncementsQueryResponse>>> Handle(GetAllAnnouncementsQueryRequest request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null) 
            return new ErrorDataResult<List<GetAllAnnouncementsQueryResponse>>("Access Token bulunamadı.");
        
        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId is null) 
            return new ErrorDataResult<List<GetAllAnnouncementsQueryResponse>>( "Kullanıcı girişi yapın.");
        
        var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        if (!roles.Contains("admin") && !roles.Contains("manager") && !roles.Contains("salesperson") && !roles.Contains("coordinator")) 
            return new ErrorDataResult<List<GetAllAnnouncementsQueryResponse>>("Yetkisiz erişim.");

        var announcements = await repository.GetAllAsync();
        if (announcements is null)
            return new ErrorDataResult<List<GetAllAnnouncementsQueryResponse>>("Duyuru bulunamadı.");
        var response = mapper.Map<List<GetAllAnnouncementsQueryResponse>>(announcements);
        
        return new SuccessDataResult<List<GetAllAnnouncementsQueryResponse>>(response);
    }
}