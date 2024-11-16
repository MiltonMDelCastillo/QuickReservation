using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickReservation.Data;
using QuickReservation.Models;
using System.Linq;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly QuickAppDbContext _context;

    public AdminController(QuickAppDbContext context)
    {
        _context = context;
    }

    // Acción para listar usuarios
    public IActionResult Users()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    public IActionResult Index()
    {
        return View();
    }
    // Crear Usuario - GET
    public IActionResult CreateUser()
    {
        return View();
    }

    // Crear Usuario - POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Users));
        }
        return View(user);
    }
}
