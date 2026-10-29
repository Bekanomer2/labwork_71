using labwork_71.BLL.Services.Interfaces;
using labwork_71.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace labwork_71.Controllers;

[Authorize(Roles = "admin")]
public class DishController : Controller
{
    private readonly IDishService _dishService;
    private readonly ICafeService _cafeService;

    public DishController(IDishService dishService, ICafeService cafeService)
    {
        _dishService = dishService;
        _cafeService = cafeService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Dish> dishes = await _dishService.GetAllAsync();

        return View(dishes);
    }
    
    [HttpGet]
    public async Task<IActionResult> Create(int? cafeId)
    {
        ViewBag.Cafes = await _cafeService.GetAllAsync();
        ViewBag.SelectedCafeId = cafeId;

        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string? name, double price, string? description, int cafeId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError("name", "Введите название блюда.");
        }

        if (price <= 0)
        {
            ModelState.AddModelError("price", "Введите цену больше 0.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            ModelState.AddModelError("description", "Введите описание блюда.");
        }

        Cafe? cafe = await _cafeService.GetByIdAsync(cafeId);

        if (cafe == null)
        {
            ModelState.AddModelError("cafeId", "Выберите заведение.");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Cafes = await _cafeService.GetAllAsync();
            return View();
        }

        Dish dish = new Dish
        {
            Name = name!,
            Price = price,
            Description = description!,
            CafeId = cafeId
        };

        await _dishService.AddAsync(dish);

        return RedirectToAction("Details", "Cafe", new { id = cafeId });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, int? cafeId)
    {
        await _dishService.DeleteAsync(id);
        
        if (cafeId.HasValue)
        {
            return RedirectToAction("Details", "Cafe", new { id = cafeId.Value });
        }

        return RedirectToAction("Index");
    }
}