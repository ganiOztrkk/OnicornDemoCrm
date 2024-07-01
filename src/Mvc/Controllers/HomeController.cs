using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Mvc.Services;

namespace Mvc.Controllers;


public class HomeController(
    AnnouncementApiService announcementApiService,
    MeetingApiService meetingApiService,
    TaskApiService taskApiService,
    IHttpContextAccessor httpContextAccessor
    ) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = httpContextAccessor.HttpContext!.Request.Cookies["UserId"];
        var announcements = await announcementApiService.GetAllAsync();
        var meetings = await meetingApiService.GetByIdAsync(Guid.Parse(userId!));
        var tasks = await taskApiService.GetByIdAsync(Guid.Parse(userId!));
        var homeDataModel = new HomeDataDto
        {
            Announcements = announcements!.Data,
            Meetings = meetings.Data,
            Tasks = tasks.Data
        };
        return View(homeDataModel);
    }
}