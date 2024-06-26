using Microsoft.AspNetCore.Mvc;
using Mvc.Services;

namespace Mvc.Controllers;

public class CustomersController(CustomerApiService customerApiService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var apiResponse = await customerApiService.GetAllAsync();
        return View(apiResponse);
    }
}