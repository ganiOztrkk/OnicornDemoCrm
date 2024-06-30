using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Mvc.Models.Task;
using Mvc.Models.User;
using Mvc.Services;

namespace Mvc.Controllers;

public class TasksController(
    TaskApiService taskApiService
    ) : Controller
{
    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var apiResponse = await taskApiService.GetAllAsync();
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
    public async Task<IActionResult> Create(CreateTaskDto createTaskDto)
    {
        var apiResponse = await taskApiService.CreateAsync(createTaskDto);
        if (apiResponse!.Success) 
            return Json(new { success = true, message = apiResponse.Message });

        ModelState.AddModelError(string.Empty, apiResponse.Message);
        return Json(new { success = false, message = apiResponse.Message });
    }
    
    [HttpPost]
    public async Task<IActionResult> GetById([FromBody] GetByIdRequest request)
    {
        var apiResponse = await taskApiService.GetByIdAsync(request.Id);
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
        var apiResponse = await taskApiService.DeleteAsync(request.Id);
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
    public async Task<IActionResult> Update(UpdateTaskDto updateTaskDto)
    {
        var apiResponse = await taskApiService.UpdateAsync(updateTaskDto);
        if (apiResponse!.Success) 
            return Json(new { success = true, message = apiResponse.Message });

        ModelState.AddModelError(string.Empty, apiResponse.Message);
        return Json(new { success = false, message = apiResponse.Message });
    }
}