using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickReservation.Data;
using QuickReservation.Models;

public class UserController : Controller
{
    private readonly QuickAppDbContext _context;

    public UserController(QuickAppDbContext context)
    {
        _context = context;
    }

    // Listar Usuarios
    public IActionResult Users()
    {
        var users = _context.Users.Include(u => u.Role).ToList(); // Incluye el rol asociado para mostrar si es necesario
        return View(users);
    }

    // Crear Usuario - GET
    public IActionResult CreateUser()
    {
        ViewBag.Roles = _context.Roles.ToList(); // Cargar roles para un dropdown en la vista, si es necesario
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
            TempData["Message"] = "¡Usuario creado exitosamente!";
            return RedirectToAction(nameof(Users));
        }

        // Si el modelo no es válido, recarga la lista de roles
        ViewBag.Roles = _context.Roles.ToList();
        return View(user);
    }

    // Editar Usuario - GET
    // Editar Usuario - GET
    public IActionResult EditUser(int id)
    {
        var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        ViewBag.Roles = _context.Roles.ToList(); // Cargar roles para la edición
        return View(user);
    }


    // Editar Usuario - POST
    // Editar Usuario - POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(User user)
    {
        if (ModelState.IsValid)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                // Actualizar propiedades
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.PasswordHash = user.PasswordHash; // Cambia si es necesario
                existingUser.RoleId = user.RoleId;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
                TempData["Message"] = "¡Usuario actualizado exitosamente!";
                return RedirectToAction(nameof(Users));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "El usuario no existe.");
            }
        }

        // Si hay errores, recargar la lista de roles
        ViewBag.Roles = _context.Roles.ToList();
        return View(user);
    }

    // Eliminar Usuario - GET
    public IActionResult DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // Eliminar Usuario - POST
    [HttpPost, ActionName("DeleteUser")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUserConfirmed(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            TempData["Message"] = "¡Usuario eliminado exitosamente!";
        }
        return RedirectToAction(nameof(Users));
    }
}
