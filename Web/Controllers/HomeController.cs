using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Tools;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IOutput output1, IOutput output2){
        Infrastructure.Class1 c = new Infrastructure.Class1();
        _logger = logger;
        output1.Print("Uno");
        // uuid Uno
        output2 = new Output();
        output2.Print("Dos");
        // uuid Dos
    }

    public IActionResult Index(){
        ViewData["nav"] = "home";
        return View();
    }

    public IActionResult Privacy(){
        ViewData["nav"] = "privacy";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
