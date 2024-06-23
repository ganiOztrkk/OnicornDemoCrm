using Core.ResultPattern;
using MediatR;

namespace Application.Features.User.Create;

public class CreateUserCommandRequest : IRequest<IResult>
{
    public string Name { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}