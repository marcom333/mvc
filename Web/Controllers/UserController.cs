using Application.Entities;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        ViewData["nav"] = "user";
        return View(_userService.GetUsers());
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        ViewData["nav"] = "user";
        return View();
    }
    
    [HttpPost]
    public IActionResult Store(User user)    
    {
        ViewData["nav"] = "product";
        if (!ModelState.IsValid)
        {
            TempData["error"] = "El usuario no fue registrado!";
            return View("Create", user);
        }

        if (_userService.CreateUser(user))
        {
            TempData["success"] = "El usuario fue registrado Exitosamente!";
            return RedirectToAction(nameof(Index));
        }
        else
        {            
            TempData["error"] = "Ocurrio un error. El usuario no fue registrado!";
            return RedirectToAction(nameof(Index));
        }
    }
    
    [HttpGet]
    public IActionResult Details()
    {
        ViewData["nav"] = "user";

        User user = new User()
        {
          UserId = 1,
          Name = "César Javier",
          Primer_Apellido = "Maldonado",
          Segundo_Apellido = "Flores"
        };

        return View(user);
    }
    
    [HttpGet("User/Edit/{id:int}")]
    public IActionResult Edit(int id)
    {    
        ViewData["nav"] = "user";
        if (id is 0)
        {
            TempData["error"] = "El usuario no fue encontrado!";
            return RedirectToAction(nameof(Index));
        }

        return View(_userService.GetUser(id));        
    }

    [HttpPost]
    public IActionResult Update(User user)
    {
        ViewData["nav"] = "user";
        if (!ModelState.IsValid)
        {
            TempData["error"] = "El usuario no fue actualizado!";
            return View("Edit", user);
        }
        
        if (_userService.UpdateUser(user))
        {
            TempData["success"] = "El usuario fue actualizado Exitosamente!";
            return RedirectToAction(nameof(Index));
        }
        else
        {            
            TempData["error"] = "Ocurrio un error. El usuario no fue actualizado!";
            return RedirectToAction(nameof(Index));
        }
    }
}