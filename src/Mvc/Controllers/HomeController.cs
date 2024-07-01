using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Mvc.Services;

namespace Mvc.Controllers;


public class HomeController(
    AnnouncementApiService announcementApiService,
    MeetingApiService meetingApiService,
    TaskApiService taskApiService,
    TicketApiService ticketApiService,
    IHttpContextAccessor httpContextAccessor
    ) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = httpContextAccessor.HttpContext!.Request.Cookies["UserId"];
        var announcements = await announcementApiService.GetAllAsync();
        var meetings = await meetingApiService.GetByIdAsync(Guid.Parse(userId!));
        var tasks = await taskApiService.GetByIdAsync(Guid.Parse(userId!));
        var tickets = await ticketApiService.GetAllAsync();
        var homeDataModel = new HomeDataDto
        {
            Announcements = announcements!.Data,
            Meetings = meetings.Data,
            Tasks = tasks.Data,
            Tickets = tickets!.Data,
            UserId = userId!
        };
        return View(homeDataModel);
    }
}