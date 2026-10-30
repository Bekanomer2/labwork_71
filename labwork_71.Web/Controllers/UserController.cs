using labwork_71.BLL.Services.Interfaces;
using labwork_71.DAL.Repositories.Interfaces;
using labwork_71.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace labwork_71.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IOrderService _orderService;
    public UserController(UserManager<User> userManager,  IOrderService orderService)
    {
        _userManager = userManager;
        _orderService = orderService;
    }
    
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("Login", "Account");

        return View(user);
    }

    public async Task<IActionResult> MyOrders()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("Login", "Account");
        List<Order> orders = await _orderService.GetUserOrdersAsync(user.Id);
        return View(orders);
    }
}
