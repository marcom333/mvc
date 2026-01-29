using Application.Entities;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Web.Tools;

namespace Web.Controllers;

public class UserController : Controller {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
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
        p.Password = "";
        return View(p);
    }
    
    public IActionResult Create() {
        return View(new User());
    }

    [HttpPost]
    public async Task<IActionResult> Create(int id, User user) {
        if(user.Name == "") return BadRequest();
        if(user.Password == "") return BadRequest();
        if(user.Email == "") return BadRequest();
        user.Password = Hasher.Hash(user);
        User product = await _userService.CreateUser(user);
        return RedirectToAction("Detail", new {id=user.UserId});
    }

    public async Task<IActionResult> Update(int id) {
        User? p = await _userService.GetUser(id);
        if(p == null) return NotFound();
        return View(p);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, User user) {
        user.UserId = id;
        if(user.Name == "") return BadRequest();
        if(user.Password == "") return BadRequest();
        if(user.Email == "") return BadRequest();
        user.Password = Hasher.Hash(user);
        await _userService.UpdateUser(user);
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