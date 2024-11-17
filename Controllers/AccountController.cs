using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using QuickReservation.Data;
using System.Security.Claims;

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
        var user = _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, GetRoleName(user.RoleId))
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            if (user.RoleId == 1)
                return RedirectToAction("Index", "Admin");
            else if (user.RoleId == 2)
                return RedirectToAction("Index", "Manager");
            else if (user.RoleId == 3)
                return RedirectToAction("Index", "Cliente");
        }

        ViewBag.ErrorMessage = "Invalid credentials";
        return View();
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    private string GetRoleName(int roleId)
    {
        return roleId switch
        {
            1 => "Admin",
            2 => "Manager",
            3 => "Cliente",
            _ => "Unknown"
        };
    }
}
