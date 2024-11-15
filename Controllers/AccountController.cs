using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using QuickReservation.Data;
using System.Linq;

public class AccountController : Controller
{
    private readonly QuickAppDbContext _context;

    public AccountController(QuickAppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        // Buscar usuario en la base de datos
        var user = _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, GetRoleName(user.RoleId)) // Obtener el nombre del rol según el RoleId
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // Redirigir según el rol
            if (user.RoleId == 1)
                return RedirectToAction("Index", "Admin");
            else if (user.RoleId == 2)
                return RedirectToAction("Index", "Gestor");
            else if (user.RoleId == 3)
                return RedirectToAction("Index", "Cliente");
        }

        // Si el login falla
        ViewBag.ErrorMessage = "Credenciales inválidas";
        return View();
    }

    private string GetRoleName(int roleId)
    {
        // Puedes usar una lógica más avanzada si tus roles están en otra tabla
        return roleId switch
        {
            1 => "Admin",
            2 => "Gestor",
            3 => "Cliente",
            _ => "Unknown"
        };
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
