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

    public IActionResult Users()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CreateUser()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Usuario creado exitosamente!";
            return RedirectToAction(nameof(Users));
        }
        return View(user);
    }

    public IActionResult EditUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Usuario actualizado exitosamente!";
            return RedirectToAction(nameof(Users));
        }
        return View(user);
    }

    public IActionResult DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost, ActionName("DeleteUser")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUserConfirmed(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Usuario eliminado exitosamente!";
        }
        return RedirectToAction(nameof(Users));
    }
}
