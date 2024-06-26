using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Mvc.Services;

namespace Mvc.Controllers;

[AllowAnonymous]
public class AuthController(AuthApiService authApiService) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto request)
    {
        var response = await authApiService.LoginAsync(request);
        if (response!.Success) return RedirectToAction("Index", "Home");

        ModelState.AddModelError(string.Empty, response.Message);
        return View(request);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        authApiService.Logout();
        return RedirectToAction("Login");
    }
}