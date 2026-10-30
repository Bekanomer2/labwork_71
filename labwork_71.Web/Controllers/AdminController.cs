using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace labwork_71.Controllers;

[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}