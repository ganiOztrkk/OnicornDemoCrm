using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Mvc.Models.Customer;
using Mvc.Models.User;
using Mvc.Services;

namespace Mvc.Controllers;

public class UsersController(UserApiService userApiService) : Controller
{
    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var apiResponse = await userApiService.GetAllAsync();
        var serializedData = Newtonsoft.Json.JsonConvert.SerializeObject(apiResponse!.Data);
        if (apiResponse!.Success) 
            return Json(new
            {
                succcess= true,
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
    public async Task<IActionResult> Create(CreateUserDto createUserDto)
    {
        var apiResponse = await userApiService.CreateAsync(createUserDto);
        if (apiResponse!.Success) 
            return Json(new { success = true, message = apiResponse.Message });

        ModelState.AddModelError(string.Empty, apiResponse.Message);
        return Json(new { success = false, message = apiResponse.Message });
    }
    
    [HttpPost]
    public async Task<IActionResult> GetById([FromBody] GetByIdRequest request)
    {
        var apiResponse = await userApiService.GetByIdAsync(request.Id);
        var serializedData = Newtonsoft.Json.JsonConvert.SerializeObject(apiResponse!.Data);
        if (apiResponse!.Success) 
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
        var apiResponse = await userApiService.DeleteAsync(request.Id);
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
    public async Task<IActionResult> Update(UpdateCustomerDto updateCustomerDto)
    {
        var apiResponse = await userApiService.UpdateAsync(updateCustomerDto);
        if (apiResponse!.Success) 
            return Json(new { success = true, message = apiResponse.Message });

        ModelState.AddModelError(string.Empty, apiResponse.Message);
        return Json(new { success = false, message = apiResponse.Message });
    }
}