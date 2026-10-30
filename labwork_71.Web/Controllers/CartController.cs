using labwork_71.BLL.Services.Interfaces;
using labwork_71.Extensions;
using labwork_71.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace labwork_71.Controllers;

public class CartController : Controller
{
    private readonly IDishService _dishService;

    public CartController(IDishService dishService)
    {
        _dishService = dishService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(int dishId)
    {
        var dish = await _dishService.GetByIdAsync(dishId);
        if (dish == null)
            return NotFound();

        var cart = HttpContext.Session.GetObject<CartViewModel>("Cart");

        if (cart == null)
        {
            cart = new CartViewModel
            {
                CafeId = dish.CafeId
            };
        }

        if (cart.CafeId != dish.CafeId)
        {
            return BadRequest(new
            {
                success = false,
                message = "Нельзя добавлять из разных кафе в одну корзину"
            });
        }

        var cartItem = cart.Items.FirstOrDefault(item => item.DishId == dish.Id);
        if (cartItem != null)
        {
            cartItem.Quantity++;
        }
        else
        {
            cart.Items.Add(new CartItemViewModel
            {
                DishId = dish.Id,
                Name = dish.Name,
                Price = dish.Price,
                Quantity = 1
            });
        }
        HttpContext.Session.SetObject("Cart", cart);

        return Json(new
        {
            success = true,
            message = "Блюдо добавлено в корзину",
            totalPrice = cart.TotalPrice,
            totalCount = cart.Items.Sum(item => item.Quantity)
        });
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int dishId)
    {
        var cart = HttpContext.Session.GetObject<CartViewModel>("Cart");
        if (cart == null)
            return NotFound();

        var cartItem = cart.Items.FirstOrDefault(item => item.DishId == dishId);
        if (cartItem == null)
            return NotFound();

        if (cartItem.Quantity > 1)
        {
            cartItem.Quantity--;
        }
        else
        {
            cart.Items.Remove(cartItem);
        }

        if (cart.Items.Count == 0)
        {
            cart.CafeId = 0;
        }

        HttpContext.Session.SetObject("Cart", cart);

        return Json(new
        {
            success = true,
            totalPrice = cart.TotalPrice,
            totalCount = cart.Items.Sum(item => item.Quantity)
        });
    }

    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObject<CartViewModel>("Cart");
        if (cart == null)
        {
            cart = new CartViewModel();
        }
        return View(cart);
    }

    [HttpGet]
    public IActionResult GetCart()
    {
        var cart = HttpContext.Session.GetObject<CartViewModel>("Cart");
        if (cart == null)
        {
            cart = new CartViewModel();
        }

        return PartialView("_CartPartial", cart);
    }
}
