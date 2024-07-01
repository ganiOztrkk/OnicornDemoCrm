namespace Mvc.Models.Ticket;

public class GetByTicketIdDto
{
    public Guid Id { get; set; }
    public Guid AppUserId { get; set; }
    public Guid TicketId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
}