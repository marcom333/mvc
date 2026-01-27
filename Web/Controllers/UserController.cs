using System.Threading.Tasks;
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

    public async Task<IActionResult> Index()
    {
        ViewData["nav"] = "user";
        return View(await _userService.GetUsers());
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        ViewData["nav"] = "user";
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Store(User user)    
    {
        ViewData["nav"] = "product";
        if (!ModelState.IsValid)
        {
            TempData["error"] = "El usuario no fue registrado!";
            return View("Create", user);
        }

        if (await _userService.CreateUser(user))
            TempData["success"] = "El usuario fue registrado Exitosamente!";
        else
            TempData["error"] = "Ocurrio un error. El usuario no fue registrado!";

        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        ViewData["nav"] = "user";
        User user = await _userService.GetUser(id);
        return View(user);
    }
    
    [HttpGet("User/Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {    
        ViewData["nav"] = "user";
        if (id is 0)
        {
            TempData["error"] = "El usuario no fue encontrado!";
            return RedirectToAction(nameof(Index));
        }

        return View(await _userService.GetUser(id));        
    }

    [HttpPost]
    public async Task<IActionResult> Update(User user)
    {
        ViewData["nav"] = "user";
        if (!ModelState.IsValid)
        {
            TempData["error"] = "El usuario no fue actualizado!";
            return View("Edit", user);
        }
        
        if (await _userService.UpdateUser(user))
            TempData["success"] = "El usuario fue actualizado Exitosamente!";
        else
            TempData["error"] = "Ocurrio un error. El usuario no fue actualizado!";
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        ViewData["nav"] = "user";        
        if (await _userService.DeleteUser(id))
            TempData["success"] = "El usuario fue eliminado Exitosamente!";
        else 
            TempData["error"] = "Ocurrio un error. El usuario no fue eliminado!";
        
        return RedirectToAction(nameof(Index));
    }
}