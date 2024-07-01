namespace Mvc.Models.Ticket;

public class GetByTicketIdDto
{
    public Guid Id { get; set; }
    public Guid AppUserId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public bool IsDeleted { get; set; }
}