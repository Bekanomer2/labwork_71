using labwork_71.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace labwork_71.Controllers;

public class ValidationController : Controller
{
    private readonly IValidatorService _validator;
    public ValidationController(IValidatorService validator)
    {
        _validator = validator;
    }
    [AcceptVerbs("GET", "POST")]
    public async Task<IActionResult> CheckEmail(string? email, int? id)
    {
        bool isValid = await _validator.CheckEmail(email, id);
        return Json(isValid);
    }
    [AcceptVerbs("GET", "POST")]
    public IActionResult CheckPassword(string? password)
    {
        bool isValid = _validator.CheckPassword(password);
        return Json(isValid);
    }
}