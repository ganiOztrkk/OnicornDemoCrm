using Core.ResultPattern;
using MediatR;

namespace Application.Features.Auth.Login;

public sealed record LoginCommandRequest(
    string EmailOrUserName,
    string Password) : IRequest<IDataResult<string>>;