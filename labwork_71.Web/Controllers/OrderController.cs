using labwork_71.BLL.Services.Interfaces;
using labwork_71.Extensions;
using labwork_71.Models;
using labwork_71.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace labwork_71.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly UserManager<User> _userManager;

    public OrderController(IOrderService orderService, UserManager<User> userManager)
    {
        _orderService = orderService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> MyOrders()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction("Login", "Account");
        var orders = await _orderService.GetUserOrdersAsync(user.Id);
        return View(orders);
    }

    [HttpGet]
    public IActionResult AddOrder()
    {
        var cart = HttpContext.Session.GetObject<CartViewModel>("Cart");
        if (cart == null || !cart.Items.Any())
            return RedirectToAction("Index", "Cafe");

        ViewBag.Cart = cart;
        return View(new OrderCreateViewModel { CafeId = cart.CafeId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrder(OrderCreateViewModel model)
    {
        var cart = HttpContext.Session.GetObject<CartViewModel>("Cart");

        if (cart == null || !cart.Items.Any())
            return RedirectToAction("Index", "Cafe");

        if (!ModelState.IsValid)
        {
            ViewBag.Cart = cart;
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);

        var order = new Order
        {
            UserId = user!.Id,
            CafeId = cart.CafeId,
            RecipientName = model.RecipientName,
            RecipientPhone = model.RecipientPhone,
            DeliveryAddress = model.DeliveryAddress,
            OrderDate = DateTime.UtcNow
        };

        var orderItems = cart.Items.Select(item => new OrderItem
        {
            ProductId = item.DishId,
            Quantity = item.Quantity
        }).ToList();

        await _orderService.AddOrderAsync(order, orderItems);

        HttpContext.Session.Remove("Cart");

        return RedirectToAction("Success");
    }

    public IActionResult Success() => View();
}
