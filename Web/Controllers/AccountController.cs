using Microsoft.AspNetCore.Mvc;
using Application.Entities;
using Application.Interface.Service;
using System.Threading.Tasks;
using Web.Tools;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers;

public class AccountController: Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }
    //Login - Get / Mostrar formulario 
    [AllowAnonymous]
    public IActionResult Login()
    {
        ViewData["nav"] = "account";
        return View();
    }

    //Login(user, contraseña) - Post / Iniciar sesion
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
    {
        ViewData["nav"] = "account";
        User user = await _userService.GetUserByEmail(email);
        
        if (user != null && Hasher.Verify(user, password))
        {
            // si se logea
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.NombreCompleto),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.UserId.ToString()),
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );

            return RedirectToAction("Index", "Home");
        }

        // no se logea
        return View();
    }

    //Logout - Get
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}