using Core.ResultPattern;
using Domain.Entities;
using MediatR;

namespace Application.Features.User.GetSellers;

public class GetSellersQueryRequest : IRequest<IDataResult<List<AppUser>>>
{
    
}