namespace Mvc.Models;

public class CreateUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}