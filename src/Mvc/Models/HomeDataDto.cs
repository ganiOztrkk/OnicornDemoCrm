using Mvc.Models.Announcement;
using Mvc.Models.Meeting;
using Mvc.Models.Task;
using Mvc.Models.Ticket;

namespace Mvc.Models;

public class HomeDataDto
{
    public List<GetAllAnnouncementDto>? Announcements { get; set; }
    public List<GetMeetingByUserIdDto>? Meetings { get; set; }
    public List<GetTaskByUserIdDto>? Tasks { get; set; }
    public List<GetAllTicketDto>? Tickets{ get; set; }
    public string UserId { get; set; } = string.Empty;
}