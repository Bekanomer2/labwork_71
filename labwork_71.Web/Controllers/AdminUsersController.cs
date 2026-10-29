using labwork_71.BLL.Services.Interfaces;
using labwork_71.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace labwork_71.Controllers;

[Authorize(Roles = "admin")]
public class AdminUsersController : Controller
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public AdminUsersController(IUserService userService, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        _userService = userService;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<User> users = await _userService.GetAllAsync();

        Dictionary<int, string> userRoles = new Dictionary<int, string>();

        foreach (User user in users)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);

            userRoles[user.Id] = roles.FirstOrDefault() ?? "user";
        }

        ViewBag.UserRoles = userRoles;

        return View(users);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string? userName, string? email, string? password, string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            ModelState.AddModelError("userName", "Введите имя пользователя.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            ModelState.AddModelError("email", "Введите Email.");
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            ModelState.AddModelError("password", "Введите пароль.");
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            User? existingUserByEmail = await _userManager.FindByEmailAsync(email);

            if (existingUserByEmail != null)
            {
                ModelState.AddModelError("email", "Пользователь с таким Email уже существует.");
            }
        }

        if (!string.IsNullOrWhiteSpace(userName))
        {
            User? existingUserByName = await _userManager.FindByNameAsync(userName);

            if (existingUserByName != null)
            {
                ModelState.AddModelError("userName", "Пользователь с таким именем уже существует.");
            }
        }

        if (!ModelState.IsValid)
        {
            return View();
        }

        User user = new User
        {
            UserName = userName,
            Email = email,
            PhoneNumber = phoneNumber,
            AvatarUrl = "/default.jpg"
        };

        IdentityResult result = await _userManager.CreateAsync(user, password!);

        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        await _userManager.AddToRoleAsync(user, "user");

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        User? user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
        {
            return NotFound();
        }

        if (user.UserName == "admin")
        {
            return RedirectToAction("Index");
        }

        await _userManager.DeleteAsync(user);

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeRole(int id, string role)
    {
        User? user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
        {
            return NotFound();
        }

        if (user.UserName == "admin")
        {
            return RedirectToAction("Index");
        }

        if (!await _roleManager.RoleExistsAsync(role))
        {
            return BadRequest();
        }

        IList<string> currentRoles = await _userManager.GetRolesAsync(user);

        if (currentRoles.Count > 0)
        {
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
        }

        IdentityResult result = await _userManager.AddToRoleAsync(user, role);

        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = $"Роль пользователя {user.UserName} изменена на {role}.";
        }

        return RedirectToAction("Index");
    }
}