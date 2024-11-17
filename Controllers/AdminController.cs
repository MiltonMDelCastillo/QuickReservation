using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickReservation.Data;
using QuickReservation.Models;
using System.Linq;
using System.Threading.Tasks;

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

    // Crear usuario - GET
    public IActionResult CreateUser()
    {
        return View();
    }

    // Crear usuario - POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            TempData["Message"] = "User created successfully!";
            return RedirectToAction(nameof(Users));
        }
        return View(user);
    }
    // Editar usuario - GET
    public IActionResult EditUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // Editar usuario - POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            TempData["Message"] = "User updated successfully!";
            return RedirectToAction(nameof(Users));
        }
        return View(user);
    }

    // Eliminar usuario - GET
    public IActionResult DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // Eliminar usuario - POST
    [HttpPost, ActionName("DeleteUser")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUserConfirmed(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            TempData["Message"] = "User deleted successfully!";
        }
        return RedirectToAction(nameof(Users));
    }
}
