using labwork_71.BLL.Services.Interfaces;
using labwork_71.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace labwork_71.Controllers;

[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IOrderService _orderService;

    public AdminController(UserManager<User> userManager, IOrderService orderService)
    {
        _userManager = userManager;
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Orders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return View(orders);
    }
}
