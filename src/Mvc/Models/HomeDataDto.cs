using Mvc.Models.Announcement;
using Mvc.Models.Meeting;
using Mvc.Models.Task;

namespace Mvc.Models;

public class HomeDataDto
{
    public List<GetAllAnnouncementDto>? Announcements { get; set; }
    public List<GetMeetingByUserIdDto>? Meetings { get; set; }
    public List<GetTaskByUserIdDto>? Tasks { get; set; }
}