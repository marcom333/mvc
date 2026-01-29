using System.Runtime.CompilerServices;
using System.Security.Claims;
using Application.Interface.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Tools;

namespace Web.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    //Login GET mostrar el formulario
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    //Login (User, password) POST - Iniciar sesión
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] string Email, [FromForm] string password)
    {
        Application.Entities.User? user = await _userService.GetUserByEmail(Email);
        //Se puede poner un viewbag como indicador o como alerta  
        //Aquí sabemos que nuestro usuario no es nulo ↓
        if(user != null && Hasher.Verify(user, password))
            {
                //Sí se loguea
                List<Claim> claims = new List<Claim>
                {
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
            // No se loguea
            return View();
    }

    //LOgout() GET - 
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}