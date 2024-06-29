using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Mvc.Services;

namespace Mvc.Controllers;

public class CustomersController(CustomerApiService customerApiService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var apiResponse = await customerApiService.GetAllAsync();
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
    public async Task<IActionResult> Create(CreateCustomerDto createCustomerDto)
    {
        var apiResponse = await customerApiService.CreateAsync(createCustomerDto);
        if (apiResponse!.Success) 
            return Json(new { success = true, message = apiResponse.Message });

        ModelState.AddModelError(string.Empty, apiResponse.Message);
        return Json(new { success = false, message = apiResponse.Message });
    }
    
    [HttpPost]
    public async Task<IActionResult> GetById([FromBody] GetByIdRequest request)
    {
        var apiResponse = await customerApiService.GetByIdAsync(request.Id);
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
        var apiResponse = await customerApiService.DeleteAsync(request.Id);
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
        var apiResponse = await customerApiService.UpdateAsync(updateCustomerDto);
        if (apiResponse!.Success) 
            return Json(new { success = true, message = apiResponse.Message });

        ModelState.AddModelError(string.Empty, apiResponse.Message);
        return Json(new { success = false, message = apiResponse.Message });
    }
}