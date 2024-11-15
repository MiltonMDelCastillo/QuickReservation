using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Cliente")]
public class ClienteController : Controller
{
    public IActionResult Index()
    {
        return View(); // Crea la vista correspondiente en Views/Cliente/Index.cshtml
    }
}
