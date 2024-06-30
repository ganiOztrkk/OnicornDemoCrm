using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Mvc.Models.Meeting;
using Mvc.Models.Task;
using Mvc.Services;

namespace Mvc.Controllers;

public class MeetingsController(
    MeetingApiService meetingApiService
    ) : Controller
{
    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var apiResponse = await meetingApiService.GetAllAsync();
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
    public async Task<IActionResult> Create(CreateMeetingDto createMeetingDto)
    {
        var apiResponse = await meetingApiService.CreateAsync(createMeetingDto);
        if (apiResponse!.Success) 
            return Json(new { success = true, message = apiResponse.Message });

        ModelState.AddModelError(string.Empty, apiResponse.Message);
        return Json(new { success = false, message = apiResponse.Message });
    }
    
    [HttpPost]
    public async Task<IActionResult> GetById([FromBody] GetByIdRequest request)
    {
        var apiResponse = await meetingApiService.GetByIdAsync(request.Id);
        var serializedData = Newtonsoft.Json.JsonConvert.SerializeObject(apiResponse.Data);
        if (apiResponse.Success) 
            return Json(new
            {
                success = true,
                data = serializedData,
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
    public async Task<IActionResult> Delete([FromBody] GetByIdRequest request)
    {
        var apiResponse = await meetingApiService.DeleteAsync(request.Id);
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
    
    [HttpPost]
    public async Task<IActionResult> Update(UpdateMeetingDto updateMeetingDto)
    {
        var apiResponse = await meetingApiService.UpdateAsync(updateMeetingDto);
        if (apiResponse!.Success) 
            return Json(new { success = true, message = apiResponse.Message });

        ModelState.AddModelError(string.Empty, apiResponse.Message);
        return Json(new { success = false, message = apiResponse.Message });
    }
}