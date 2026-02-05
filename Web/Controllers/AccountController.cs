

using System.Security.Claims;
using System.Threading.Tasks;
using Application.Interface.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Tools;

namespace Web.Controllers;

public class AccountController: Controller {

    private readonly IUserService _userService;

    public AccountController(IUserService userService) {
        _userService = userService;
    }
    
    // Login Get Mostrar el formulario
    [AllowAnonymous]
    public IActionResult Login() {
        return View();
    }

    // Login (user, contraseña) post iniciar sesión
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] string Email, [FromForm] string Password) {
        
        Application.Entities.User? user = await _userService.GetUserByEmail(Email);

        if (user != null && Hasher.Verify(user, Password)) {
            // si se logea
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
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

    // Logout get
    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

}