using Application.Entities;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class UserController : Controller {

    private readonly IUserService _userService;

    public UserController(IUserService productService) {
        _userService = productService;
    }

    public async Task<IActionResult> Index(string? name) {
        if(name == "") name = null;
        ViewBag.SearchName = name;
        List<User> users = await _userService.GetUsers(name);
        return View(users);
    }
    
    public async Task<IActionResult> Detail(int id) {
        User? p = await _userService.GetUser(id);
        if(p == null) return NotFound();
        return View(p);
    }
    
    public IActionResult Create() {
        return View(new User());
    }

    [HttpPost]
    public async Task<IActionResult> Create(User p) {
        if(p.Name == "") return BadRequest();
        if(p.Password == "") return BadRequest();
        if(p.Email == "") return BadRequest();
        TempData["status"] = 200;
        User product = await _userService.CreateUser(p);
        return RedirectToAction("Detail", new {id=product.UserId});
    }

    public async Task<IActionResult> Update(int id) {
        User? p = await _userService.GetUser(id);
        if(p == null) return NotFound();
        return View(p);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, User p) {
        p.UserId = id;
        if(p.Name == "") return BadRequest();
        if(p.Password == "") return BadRequest();
        if(p.Email == "") return BadRequest();
        await _userService.UpdateUser(p);
        return RedirectToAction("Detail", "User", new {id, name="hola", registrado=true});
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id) {
        User? p = await _userService.GetUser(id);
        if(p == null) return NotFound();
        await _userService.DeleteUser(p);
        return RedirectToAction("Index");
    }
}