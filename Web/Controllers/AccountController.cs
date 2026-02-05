using Microsoft.AspNetCore.Mvc;
using Web.Controllers;

public class AccountController : Controller {
 //login Get mostrar el formulario
 [HttpGet("Account/Login")]
    public IActionResult Login() {
        return View();
    }
    //login (User ,Contraseña)  Post Iniciar sesión
    [HttpPost("Account/Login")]   
    [ValidateAntiForgeryToken]
    public IActionResult Login([FromForm] string Email, [FromForm] string password) {
        if(Email == "admin@admin.com" && password == "admin") {
            //crear sesión
            HttpContext.Session.SetString("user", Email);
            return RedirectToAction("Index", "Home");
        }
        ViewBag.Error = "Usuario o contraseña incorrecta";
        return View();
    }   

    //logout get cerrar sesión
    [HttpGet("Account/Logout")]
    public IActionResult Logout() {
        HttpContext.Session.Remove("user");
        return RedirectToAction("Index", "Home");
    }
}