using Microsoft.AspNetCore.Mvc;

namespace Mvc.Controllers;

public class UsersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create()
    {
        return Json("");
    }
    
    [HttpPost]
    public async Task<IActionResult> Update()
    {
        return Json("");
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete()
    {
        return Json("");
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Json("");
    }
    
    [HttpPost]
    public async Task<IActionResult> GetById()
    {
        return Json("");
    }
}