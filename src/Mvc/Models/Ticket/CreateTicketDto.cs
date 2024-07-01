namespace Mvc.Models.Ticket;

public class CreateTicketDto
{
    public string Subject { get; set; } = string.Empty; 
    public string Summary { get; set; } = string.Empty;
}