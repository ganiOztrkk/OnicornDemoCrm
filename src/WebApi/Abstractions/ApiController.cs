using Microsoft.AspNetCore.Mvc;

namespace WebApi.Abstractions;

[Route("api/[controller]/[action]")]
[ApiController]
public abstract class ApiController : ControllerBase
{
}