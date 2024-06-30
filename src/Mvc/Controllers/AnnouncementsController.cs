using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Mvc.Models.Announcement;
using Mvc.Services;

namespace Mvc.Controllers;

public class AnnouncementsController(AnnouncementApiService announcementApiService) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var apiResponse = await announcementApiService.GetAllAsync();
        var serializedData = Newtonsoft.Json.JsonConvert.SerializeObject(apiResponse!.Data);
        if (apiResponse.Success) 
            return Json(new
            {
                success= true,
                data= serializedData,
                message = apiResponse.Message 
            });

        ModelState.AddModelError(string.Empty, apiResponse.Message);
        return Json(new
        {
            success = false, 
            message = apiResponse.Message
        });
    }

    
    [HttpPost]
    public async Task<IActionResult> Create(CreateAnnouncementDto createAnnouncementDto)
    {
        var apiResponse = await announcementApiService.CreateAsync(createAnnouncementDto);
        if (apiResponse!.Success) 
            return Json(new { success = true, message = apiResponse.Message });

        ModelState.AddModelError(string.Empty, apiResponse.Message);
        return Json(new { success = false, message = apiResponse.Message });
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] GetByIdRequest request)
    {
        var apiResponse = await announcementApiService.DeleteAsync(request.Id);
        if (apiResponse!.Success) 
            return Json(new
            {
                success = true,
                message = apiResponse.Message 
            });

        ModelState.AddModelError(string.Empty, apiResponse.Message);
        return Json(new
        {
            success = false, 
            message = apiResponse.Message
        });
    }
}