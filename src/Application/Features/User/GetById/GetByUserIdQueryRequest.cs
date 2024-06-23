using Core.ResultPattern;
using Domain.Entities;
using MediatR;

namespace Application.Features.User.GetById;

public class GetByUserIdQueryRequest : IRequest<IDataResult<AppUser>>
{
    public Guid UserId { get; set; }
}