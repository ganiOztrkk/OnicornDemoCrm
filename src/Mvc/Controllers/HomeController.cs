using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Attributes;

namespace Mvc.Controllers;


public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}