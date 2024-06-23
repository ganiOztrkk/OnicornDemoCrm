using Core.ResultPattern;
using Domain.Entities;

namespace Domain.Services;

public interface IJwtProvider
{
    Task<IDataResult<string>> CreateTokenAsync(AppUser user, List<string> roles);
}