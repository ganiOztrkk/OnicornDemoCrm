namespace Mvc.Models.Customer;

public class CreateCustomerDto
{
    public string? Name { get; set; }
    public string? CompanyName { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string? Sector { get; set; }
    public string? ShoppingArea { get; set; }
}