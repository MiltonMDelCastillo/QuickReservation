using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Gestor")]
public class GestorController : Controller
{
    public IActionResult Index()
    {
        return View(); // Crea la vista correspondiente en Views/Gestor/Index.cshtml
    }
}
