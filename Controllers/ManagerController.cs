using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Manager")]
public class ManagerController : Controller
{
    public IActionResult Index()
    {
        return View(); // Crea la vista correspondiente en Views/Gestor/Index.cshtml
    }
}
