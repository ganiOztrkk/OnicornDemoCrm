using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
    public sealed class LoginDto
    {
        public string EmailOrUserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}