namespace Mvc.Models.Ticket;

public class GetAllTicketDto
{
    public Guid Id { get; set; }
    public string Subject { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
}