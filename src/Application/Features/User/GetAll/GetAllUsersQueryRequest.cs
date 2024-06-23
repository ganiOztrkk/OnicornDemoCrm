using Core.ResultPattern;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.User.GetAll;

public class GetAllUsersQueryRequest : IRequest<IDataResult<List<AppUser>>>
{
    
}