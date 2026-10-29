using labwork_71.BLL.Services.Interfaces;
using labwork_71.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace labwork_71.Controllers;

[Authorize(Roles = "admin")]
public class CafeController : Controller
{
    private readonly ICafeService _cafeService;
    private readonly IWebHostEnvironment _environment;

    public CafeController(ICafeService cafeService, IWebHostEnvironment environment)
    {
        _cafeService = cafeService;
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Cafe> cafes = await _cafeService.GetAllAsync();

        return View(cafes);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string name, string description, IFormFile? image)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError("Name", "Введите название заведения.");
        }

        if (!ModelState.IsValid)
        {
            return View();
        }

        string avatarUrl = "/default.jpg";

        if (image != null && image.Length > 0)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);

            string folderPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "cafes"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using FileStream stream = new FileStream(filePath, FileMode.Create);

            await image.CopyToAsync(stream);

            avatarUrl = "/images/cafes/" + fileName;
        }

        Cafe cafe = new Cafe
        {
            Name = name,
            Description = description,
            AvatarUrl = avatarUrl,
            Dishes = new List<Dish>()
        };

        await _cafeService.AddAsync(cafe);

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _cafeService.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}