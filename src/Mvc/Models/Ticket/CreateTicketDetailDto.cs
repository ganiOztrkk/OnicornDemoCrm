namespace Mvc.Models.Ticket;

public class CreateTicketDetailDto
{
    public Guid TicketId { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid AppUserId { get; set; }
}