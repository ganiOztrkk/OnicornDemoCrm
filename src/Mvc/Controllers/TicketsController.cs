using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Mvc.Models.Ticket;
using Mvc.Services;

namespace Mvc.Controllers;

public class TicketsController(
    TicketApiService ticketApiService
    ) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var apiResponse = await ticketApiService.GetAllAsync();
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
    public async Task<IActionResult> GetDetail([FromBody] GetByIdRequest request)
    {
        var apiResponse = await ticketApiService.GetByIdAsync(request.Id);
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
    public async Task<IActionResult> Create(CreateTicketDto createTicketDto)
    {
        var apiResponse = await ticketApiService.CreateAsync(createTicketDto);
        if (apiResponse!.Success) 
            return Json(new { success = true, message = apiResponse.Message });

        ModelState.AddModelError(string.Empty, apiResponse.Message);
        return Json(new { success = false, message = apiResponse.Message });
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateDetail(CreateTicketDetailDto createTicketDetailDto)
    {
        var apiResponse = await ticketApiService.CreateDetailContentAsync(createTicketDetailDto);
        if (apiResponse!.Success) 
            return Json(new { success = true, message = apiResponse.Message });

        ModelState.AddModelError(string.Empty, apiResponse.Message);
        return Json(new { success = false, message = apiResponse.Message });
    }
    
    [HttpPost]
    public async Task<IActionResult> GetById([FromBody] GetByIdRequest request)
    {
        var apiResponse = await ticketApiService.GetByIdAsync(request.Id);
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
}